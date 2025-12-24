namespace api.DTOs;

public abstract record RequestTypes
{
    public record UpdatePasswordRequest(
        string CurrentPassword,
        string NewPassword,
        string NewPasswordConfirmation);

    public record ResetPasswordRequest(string UserId);

    public record RenewTokenRequest(string RefreshToken);

    public record RegisterUserRequest(
        string Email,
        string? ProfilePic,
        string? Username
    );

    public record UpdateUserRequest(
        string Role,
        string? CurrentEmail,
        string? Email,
        string? ProfilePic,
        string? Nickname
    );
}