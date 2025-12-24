
namespace api.Models;

public class UserImage : BaseEntity
{
    public UserImage(Guid userId, string fileName, DateTime uploadedAt)
    {
        UserId = userId;
        FileName = fileName;
        UploadedAt = uploadedAt;
    }

    public Guid UserId { get; set; } 
    
    public string FileName { get; set; } = null!; 
    
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow; 
    
    public virtual User User { get; set; } = null!;
}
