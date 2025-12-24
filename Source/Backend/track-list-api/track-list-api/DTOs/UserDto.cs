using api.Enums;
using api.Models;

namespace api.DTOs;

public class UserDto
{
    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public UserRole Role { get; set; }

    public string? Country { get; set; }

    public Gender Gender { get; set; }

    public string? ProfilePicUrl { get; set; }

    public IFormFile? ProfilePic { get; set; }

    public List<Review> Reviews { get; set; } = [];
    public List<Follow> Following { get; set; } = []; // На кого підписаний
    public List<Follow> Followers { get; set; } = []; // Хто підписаний
    public List<Playlist> Collections { get; set; } = []; // Власник
}