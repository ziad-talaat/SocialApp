using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<AppUser> UserRepository { get; }
        IGenericRepository<Profile> ProfileRepository { get; }
        IGenericRepository<Post>    PostRepository { get; }
        IGenericRepository<PostVote> PostVoteRepository { get; }
        IGenericRepository<PostImages> PostImagesRepository { get; }
        IGenericRepository<Comment> CommentRepository { get; }
        Task<int> SaveAsync(CancellationToken cancellationToken);


    }
}
