using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public IGenericRepository<AppUser> UserRepository { get;private set; }

        public IGenericRepository<Profile> ProfileRepository { get;private set; }

        public IGenericRepository<Post> PostRepository { get;private set; }
        public IGenericRepository<PostVote> PostVoteRepository { get;private set; }

        public IGenericRepository<Comment> CommentRepository { get; private set; }

        public IGenericRepository<PostImages> PostImagesRepository { get; set; }

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext= dbContext;
            UserRepository = new GenericRepository<AppUser>(_dbContext);
            ProfileRepository= new GenericRepository<Profile>(_dbContext);
            PostRepository= new GenericRepository<Post>(_dbContext);
            PostVoteRepository= new GenericRepository<PostVote>(_dbContext);
            CommentRepository=new GenericRepository<Comment>(_dbContext);
            PostImagesRepository=new GenericRepository<PostImages>(_dbContext);
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
          return  await  _dbContext.SaveChangesAsync();
        }
    }
}
