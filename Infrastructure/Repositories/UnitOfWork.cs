
using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IDbContextTransaction? _transaction;
        public IGenericRepository<AppUser> UserRepository { get;private set; }

        public IGenericRepository<Profile> ProfileRepository { get;private set; }

        public IGenericRepository<Post> PostRepository { get;private set; }
        public IGenericRepository<PostVote> PostVoteRepository { get;private set; }

        public IGenericRepository<Comment> CommentRepository { get; private set; }

        public IGenericRepository<PostImages> PostImagesRepository { get; set; }

        public IGenericRepository<Group> GroupRepository { get; set; }
        public IGenericRepository<GroupMember> GroupMemberRepository { get; set; }

        public IGenericRepository<ProfileFollowers> ProfileFollowersRepository { get; set; }

        public IGenericRepository<FriendShip> FriendShipRepository { get; set; }

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext= dbContext;
            UserRepository = new GenericRepository<AppUser>(_dbContext);
            ProfileRepository= new GenericRepository<Profile>(_dbContext);
            PostRepository= new GenericRepository<Post>(_dbContext);
            PostVoteRepository= new GenericRepository<PostVote>(_dbContext);
            CommentRepository=new GenericRepository<Comment>(_dbContext);
            PostImagesRepository=new GenericRepository<PostImages>(_dbContext);
            GroupRepository=new GenericRepository<Group>(_dbContext);
            GroupMemberRepository=new GenericRepository<GroupMember>(_dbContext);
            ProfileFollowersRepository=new GenericRepository<ProfileFollowers>(_dbContext);
            FriendShipRepository=new GenericRepository<FriendShip>(_dbContext);
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
          return  await  _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            _transaction =await  _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction is null)
                throw new InvalidOperationException(
                    "No active transaction.");
            try
            {

            await _transaction.CommitAsync(cancellationToken);
            }
            finally
            {

            await _transaction.DisposeAsync();
            }
            _transaction = null;
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction is null)
                return;

            try
            {

            await _transaction.RollbackAsync(cancellationToken);
            }
            finally
            {

            await _transaction.DisposeAsync();
            }
            _transaction = null;
        }
    }
}
