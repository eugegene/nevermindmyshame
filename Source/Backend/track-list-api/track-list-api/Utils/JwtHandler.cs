using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Enums;
using api.Identity;
using api.Models;
using dotenv.net;
using Microsoft.IdentityModel.Tokens;

namespace api.Utils;

internal static class JwtHandler
{
    private const string TokenTypeStr = "tokenType";
    private const string AccessStr = "access";
    private const string RefreshStr = "refresh";
    private static readonly IDictionary<string, string> Env = DotEnv.Read();

    public static Result<string> GetIdFromToken(string? jwtToken)
    {
        if (string.IsNullOrWhiteSpace(jwtToken))
            return Result.Fail<string>("jswtToken can't be null, ehitespace or empty");

        var tokenHandler = new JwtSecurityTokenHandler();
        string? userId;
        jwtToken = RemoveBearerStr(jwtToken);
        var accessTokenValidationParameters = GetValidationParameters(GetPrivateKey());

        try
        {
            if (string.IsNullOrWhiteSpace(jwtToken))
                return Result.Fail<string>($"{RefreshStr} token was null or empty.");
            var claims = tokenHandler.ValidateToken(jwtToken, accessTokenValidationParameters, out _);

            userId = claims.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (string.IsNullOrWhiteSpace(userId))
                return Result.Fail<string>("User ID claim was null or empty.");
        }
        catch (SecurityTokenException ex)
        {
            return Result.Fail<string>($"{RefreshStr} token not valid.\n{ex.Message}");
        }

        return Result.Ok(userId);
    }

    public static Result<string> ValidateRefreshTokenAndGetId(string refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        string? userId;
        var accessTokenValidationParameters = GetValidationParameters(GetPrivateKey());
        try
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                return Result.Fail<string>($"{RefreshStr} token, was null or empty.");
            var claims = tokenHandler.ValidateToken(refreshToken, accessTokenValidationParameters, out _);

            if (claims.Claims.FirstOrDefault(c => c.Type == TokenTypeStr)?.Value != RefreshStr)
                return Result.Fail<string>($"{RefreshStr} token type is not \"{RefreshStr}\".");

            userId = claims.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (string.IsNullOrWhiteSpace(userId))
                return Result.Fail<string>($"{RefreshStr} token \"id\" claim was empty.");
        }
        catch (SecurityTokenException ex)
        {
            return Result.Fail<string>($"{RefreshStr} token not valid.\n{ex.Message}");
        }

        return Result.Ok(userId);
    }

    public static Result<bool> IsRoleHigherThan(UserRole requiredRole, string? jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken))
            return Result.Fail<bool>($"{AccessStr} token, was null or empty.");

        jwtToken = RemoveBearerStr(jwtToken);

        var tokenHandler = new JwtSecurityTokenHandler();
        UserRole userRole;
        var accessTokenValidationParameters = GetValidationParameters(GetPrivateKey());
        try
        {
            var claims = tokenHandler.ValidateToken(jwtToken, accessTokenValidationParameters, out _);

            if (claims.Claims.FirstOrDefault(c => c.Type == TokenTypeStr)?.Value != AccessStr)
                return Result.Fail<bool>($"{AccessStr} token type is not \"{AccessStr}\".");

            var temp = claims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (string.IsNullOrWhiteSpace(temp))
                return Result.Fail<bool>($"{RefreshStr} token \"userRole\" claim was empty.");
            userRole = Enum.Parse<UserRole>(temp);
        }
        catch (SecurityTokenException ex)
        {
            return Result.Fail<bool>($"{AccessStr} token not valid.\n{ex.Message}");
        }

        if (!IdentityData.AccessLevelDict.ContainsKey(requiredRole)
            || !IdentityData.AccessLevelDict.ContainsKey(userRole))
            return Result.Fail<bool>("access level (required or passed) not presented in dictionary.");

        var requiredAccess = IdentityData.AccessLevelDict[requiredRole];
        var grantedAccess = IdentityData.AccessLevelDict[userRole];

        //if student level required AND student (same or lower) level passed -> false
        //if student level required BUT tutor (higher) level passed -> true
        if (grantedAccess <= requiredAccess) return Result.Ok(false);
        return Result.Ok(true);
    }

    public static Result<UserRole> GetUserRole(string? jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken))
            return Result.Fail<UserRole>($"{AccessStr} token, was null or empty/");

        jwtToken = RemoveBearerStr(jwtToken);

        var tokenHandler = new JwtSecurityTokenHandler();
        var accessTokenValidationParameters = GetValidationParameters(GetPrivateKey());
        try
        {
            var claims = tokenHandler.ValidateToken(jwtToken, accessTokenValidationParameters, out _);

            if (claims.Claims.FirstOrDefault(c => c.Type == TokenTypeStr)?.Value != AccessStr)
                return Result.Fail<UserRole>($"{AccessStr} token type is not \'{AccessStr}\'.");

            var temp = claims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (string.IsNullOrWhiteSpace(temp))
                return Result.Fail<UserRole>($"{RefreshStr} token \'userRole\' claim was empty.");
            var userRole = Enum.Parse<UserRole>(temp);
            return Result.Ok(userRole);
        }
        catch (SecurityTokenException ex)
        {
            return Result.Fail<UserRole>($"{AccessStr} token not valid.\n{ex.Message}");
        }
    }

    private static string RemoveBearerStr(string jwtToken)
    {
        const string upperBearerStr = "Bearer ";
        const string lowerBearerStr = "bearer ";

        if (jwtToken.Contains(lowerBearerStr) || jwtToken.Contains(upperBearerStr))
            jwtToken = jwtToken.Remove(0, lowerBearerStr.Length);
        return jwtToken;
    }

    internal static string GenerateAccessToken(User user)
    {
        var privateKey = GetPrivateKey();
        var claims = new List<Claim>
        {
            new(TokenTypeStr, AccessStr),
            new("id", user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        var jwtObj = new JwtSecurityToken(
            GetIssuer(),
            GetAudience(),
            notBefore: DateTime.UtcNow.AddMinutes(-1),
            expires: DateTime.UtcNow.AddMinutes(15),
            claims: claims,
            signingCredentials: credentials
        );

        var handle = new JwtSecurityTokenHandler();
        return handle.WriteToken(jwtObj);
    }

    internal static string GenerateRefreshToken(User user)
    {
        var privateKey = GetPrivateKey();
        var claims = new List<Claim>
        {
            new(TokenTypeStr, RefreshStr),
            new("id", user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        var jwtObj = new JwtSecurityToken(
            GetIssuer(),
            GetAudience(),
            notBefore: DateTime.UtcNow.AddMinutes(-1),
            expires: DateTime.UtcNow.AddDays(14),
            claims: claims,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtObj);
    }

    private static TokenValidationParameters GetValidationParameters(SymmetricSecurityKey key)
    {
        return new TokenValidationParameters
        {
            IssuerSigningKey = key,
            ValidIssuer = GetIssuer(),
            ValidAudience = GetAudience(),
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true
        };
    }

    internal static SymmetricSecurityKey GetPrivateKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env["JWT_PRIVATE_KEY"]));
    }

    internal static string GetAudience()
    {
        return Env["JWT_AUDIENCE"];
    }

    internal static string GetIssuer()
    {
        return Env["JWT_ISSUER"];
    }
}