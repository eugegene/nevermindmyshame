using System.Security.Cryptography;
using api.DbContext;
using api.DTOs;
using api.Enums;
using api.Identity;
using api.Models;
using api.Utils;
using static api.DTOs.ResponseTypes;

namespace api.Services;

public static class AuthService
{
    private const string WrongLoginOrPasswordStr = $"Wrong {nameof(UserDto.Email)} or {nameof(UserDto.Password)}.";

    public static Result<TokensResponse> LoginUser(string email, string password)
    {
        var context = ContextFactory.CreateNew();

        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            return Result.Fail<TokensResponse>(WrongLoginOrPasswordStr);

        var user = context.Users.FirstOrDefault(x => x.Email == email);

        if (user is null || !IsCorrectPassword(user, password))
            return Result.Fail<TokensResponse>(WrongLoginOrPasswordStr);

        TokensResponse response = new(
            JwtHandler.GenerateAccessToken(user),
            JwtHandler.GenerateRefreshToken(user)
        );

        return Result.Ok(response);
    }

    public static bool IsCorrectPassword(User user, string password)
    {
        return user.PasswordHash
            .Equals(BCrypt.Net.BCrypt
                .HashPassword(password, user.PasswordSalt)
            );
    }

    public static Result<string> RegisterUser(UserDto userDto, UserRole registerType)
    {
        var context = ContextFactory.CreateNew();
        if (context.Users.FirstOrDefault(x => x.Email == userDto.Email) != default)
            return Result.Fail<string>($"{nameof(User)} with this {nameof(User.Email)} already exists.");

        var generatedPasword = RandomNumberGenerator.GetString(
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789",
            10
        );

        if (string.IsNullOrWhiteSpace(userDto.Email))
            return Result.Fail<string>($"{nameof(userDto.Email)} is null, empty or white space.");
        if (string.IsNullOrWhiteSpace(userDto.Username))
            return Result.Fail<string>($"{nameof(userDto.Username)} is null, empty or white space.");


        User user = new(BCrypt.Net.BCrypt.GenerateSalt())
        {
            Email = userDto.Email,
            Username = userDto.Username
        };

        try
        {
            user.Role = registerType switch
            {
                UserRole.User => IdentityData.ClaimUser,
                UserRole.Moderator => IdentityData.ClaimModerator,
                UserRole.Admin => IdentityData.ClaimAdmin,
                _ => throw new ArgumentOutOfRangeException(nameof(registerType), registerType, null)
            };
        }
        catch
        {
            return Result.Fail<string>(
                $"Unknown {nameof(User)} {nameof(registerType)}: \"{registerType.ToString()}\".");
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(generatedPasword, user.PasswordSalt);

        var res = AddUserToDb();
        if (!res.IsSuccess) return Result.Fail<string>(res.Error);

        return Result.Ok(generatedPasword);

        Result AddUserToDb()
        {
            switch (registerType)
            {
                case UserRole.User:
                    //context.Users.Add(new Student(user));
                    break;
                case UserRole.Moderator:
                    //...
                    break;
                case UserRole.Admin:
                    //...
                    break;
                default:
                    return Result.Fail(
                        $"Unknown {nameof(User)} {nameof(registerType)}: \"{registerType.ToString()}\".");
            }

            context.SaveChanges();

            return Result.Ok();
        }
    }

    public static Result<TokensResponse> RenewToken(string jwtToken)
    {
        var context = ContextFactory.CreateNew();
        if (string.IsNullOrEmpty(jwtToken))
            return Result.Fail<TokensResponse>($"{nameof(jwtToken)} was null or empty.");

        var result = JwtHandler.ValidateRefreshTokenAndGetId(jwtToken);
        if (!result.IsSuccess) return Result.Fail<TokensResponse>(result.Error);

        var user = context.Users.Find(Guid.Parse(result.Value));
        if (user is null) return Result.Fail<TokensResponse>($"{nameof(User)} do not exist in database.");

        TokensResponse response = new(
            JwtHandler.GenerateAccessToken(user),
            JwtHandler.GenerateRefreshToken(user)
        );

        return Result.Ok(response);
    }
}