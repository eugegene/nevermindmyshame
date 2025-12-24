using api.DbContext;
using api.Repository.IReposotory;

namespace api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get; private set; }
        public IUserImageRepository UserImageRepository { get; private set; }
        public UnitOfWork(TrackListDbContext _db)
        {
            UserRepository = new UserRepository(_db);
            UserImageRepository = new UserImageRepository(_db);
        }
    }
}
