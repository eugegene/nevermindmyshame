using api.DbContext;
using api.Models;
using api.Repository.IReposotory;

namespace api.Repository
{
    public class UserImageRepository : Repository<UserImage>, IUserImageRepository
    {
        private readonly TrackListDbContext _db;

        public UserImageRepository(TrackListDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<UserImage> Update(UserImage userImage)
        {
            userImage.UpdatedAt = DateTime.UtcNow;
            _db.UserImages.Update(userImage);
            await _db.SaveChangesAsync();
            return userImage;
        }
    }
}
