using System.Security.Cryptography;
using api.DbContext;
using api.DTOs;
using api.Enums;
using api.Identity;
using api.Models;
using api.Repository;
using api.Repository.IReposotory;
using api.Utils;
using dotenv.net;

namespace api.Services;

public static class UserService
{
    private const string PasswordSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private static readonly IUnitOfWork _unitOfWork = new UnitOfWork(ContextFactory.CreateNew());
    private static readonly IDictionary<string, string> Env = DotEnv.Read();

    public static async Task<Result<string>> SavePhoto(IFormFile file, Guid userGuidId, string fileStoragePath)
    {
        if (string.IsNullOrEmpty(fileStoragePath)) return Result.Fail<string>("File storage path is not set.");

        var userResult = _unitOfWork.UserRepository.GetAsync(u => u.Id == userGuidId);
        if (userResult.Result is null) return Result.Fail<string>("User not found.");

        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), fileStoragePath);

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        string[] allowedExtensions = [".jpg", ".jpeg", ".png"];
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        var notAllowedExtension = allowedExtensions.Contains(extension);
        if (notAllowedExtension) return Result.Fail<string>("Invalid file type.");

        var fileName = Guid.NewGuid() + extension;
        var filePath = Path.Combine(uploadPath, fileName);

        //це для запису аватарки на диск
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var userImage = new UserImage(
            userGuidId,
            fileName,
            DateTime.UtcNow);

        var entity = await _unitOfWork.UserImageRepository.AddAsync(userImage);

        return Result.Ok(entity.FileName);
    }

    public static Result<ResponseTypes.UserFileResponse> GetAvatar(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return Result.Fail<ResponseTypes.UserFileResponse>("Ім'я файлу не може бути порожнім.");

        string[] allowedExtensions = [".jpg", ".jpeg", ".png"];
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        
        var notAllowedExtension = !allowedExtensions.Contains(extension);
        if (notAllowedExtension)
            return Result.Fail<ResponseTypes.UserFileResponse>("Invalid file type.");

        // Запобігання атакам шляхового обходу
        if (fileName.Contains(".."))
            return Result.Fail<ResponseTypes.UserFileResponse>("Невірний формат імені файлу.");

        var filePath = Path.Combine(Env["FILE_STORAGE_PATH"], fileName);

        if (!File.Exists(filePath))
            return Result.Fail<ResponseTypes.UserFileResponse>("Файл не знайдено.");

        // Визначаємо MIME-тип файлу
        var extension1 = Path.GetExtension(filePath).ToLowerInvariant();
        var contentType = extension1 switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".webp" => "image/webp",
            _ => "application/octet-stream"
        };

        var bytes = File.ReadAllBytes(filePath);

        return Result.Ok(new ResponseTypes.UserFileResponse(bytes, contentType));
    }

    public static async Task<Result<ResponseTypes.UserRegistrationResponse>> RegisterUser(
        RequestTypes.RegisterUserRequest userDto,
        UserRole registerRole)
    {
        if (!Enum.IsDefined(typeof(UserRole), registerRole))
            return Result.Fail<ResponseTypes.UserRegistrationResponse>(
                $"Unknown user role: \"{registerRole.ToString()}\"");
        var userFromDb = await _unitOfWork.UserRepository.GetAsync(x => x.Email == userDto.Email);
        if (userFromDb != default)
            return Result.Fail<ResponseTypes.UserRegistrationResponse>("User with this login already exists.");


        var generatedPassword = RandomNumberGenerator.GetString(
            PasswordSymbols,
            10);

        if (string.IsNullOrEmpty(userDto.Email))
            return Result.Fail<ResponseTypes.UserRegistrationResponse>(
                $"{nameof(userDto.Email)}  was null, empty or white spaces.");
        if (string.IsNullOrEmpty(userDto.Username))
            return Result.Fail<ResponseTypes.UserRegistrationResponse>(
                $"{nameof(userDto.Username)} was null, empty or white spaces.");

        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        User user = new(salt)
        {
            Email = userDto.Email,
            Username = userDto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(generatedPassword, salt)
        };

        var res = await AddUserToDb();
        if (res.IsFailure) return Result.Fail<ResponseTypes.UserRegistrationResponse>(res.Error);

        ResponseTypes.UserRegistrationResponse resp = new(generatedPassword, user.Id.ToString());

        return Result.Ok(resp);

        async Task<Result> AddUserToDb()
        {
            switch (registerRole)
            {
                case UserRole.User:
                    user.Role = IdentityData.ClaimUser;
                    await _unitOfWork.UserRepository.AddAsync(user);
                    return Result.Ok();
                default:
                    return Result.Fail($"Unknown user type: \"{registerRole.ToString()}\"");
            }
        }
    }

    public static async Task<Result<string>> ResetPassword(string userId)
    {
        var isSuccess = Guid.TryParse(userId, out var guid);
        if (!isSuccess) return Result.Fail<string>("Id is not a Guid formatted");

        var dbUser = await _unitOfWork.UserRepository.GetAsync(u => u.Id == guid);
        if (dbUser is null) return Result.Fail<string>($"User with ID \"{userId}\" doesn't exist");

        dbUser.PasswordSalt = BCrypt.Net.BCrypt.GenerateSalt();
        var generatedPassword = RandomNumberGenerator.GetString(
            PasswordSymbols,
            10);
        dbUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(generatedPassword, dbUser.PasswordSalt);

        await _unitOfWork.UserRepository.Update(dbUser);

        return Result.Ok(generatedPassword);
    }

    public static async Task<Result> UpdatePassword(string id, string newPassword, string currentPassword)
    {
        if (string.IsNullOrEmpty(id)) return Result.Fail($"{nameof(id)} was null or empty");

        var isSuccess = Guid.TryParse(id, out var guid);
        if (!isSuccess) return Result.Fail("Id is not a Guid formatted");

        var dbUser = await _unitOfWork.UserRepository.GetAsync(u => u.Id == guid);
        if (dbUser is null) return Result.Fail($"User with ID \"{id}\" doesn't exist");
        if (!AuthService.IsCorrectPassword(dbUser, currentPassword))
            return Result.Fail("currentPassword&Невірний поточний пароль");

        dbUser.PasswordSalt = BCrypt.Net.BCrypt.GenerateSalt();
        dbUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword, dbUser.PasswordSalt);

        await _unitOfWork.UserRepository.Update(dbUser);

        return Result.Ok();
    }

    public static async Task<Result> DeleteAnyUser(string userId)
    {
        var resGuid = GuidParser.TryParseGuid(userId, nameof(userId));
        if (!resGuid.IsSuccess) return Result.Fail(resGuid.Error);

        var userToDelete = await _unitOfWork.UserRepository.GetAsync(u => u.Id == resGuid.Value);
        if (userToDelete is null) return Result.Fail($"User with Id: \"{userId}\" doesn't exist");

        if (userToDelete.Role == IdentityData.ClaimAdmin) return Result.Fail("can't delete admin");

        await _unitOfWork.UserRepository.Remove(userToDelete);

        return Result.Ok();
    }
}