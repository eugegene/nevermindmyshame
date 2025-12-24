namespace api.DTOs;

public abstract record ResponseTypes
{
    public record TokensResponse(string AccessToken, string RefreshToken)
    {
        public string AccessToken { get; init; } = AccessToken ?? throw new ArgumentNullException(nameof(AccessToken));

        public string RefreshToken { get; init; } =
            RefreshToken ?? throw new ArgumentNullException(nameof(RefreshToken));
    }

    public record UserRegistrationResponse(string Password, string Id);

    public record UserFileResponse(byte[] Bytes, string ContentType);
}