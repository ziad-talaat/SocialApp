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
        IGenericRepository<Group> GroupRepository { get; }
        IGenericRepository<GroupMember> GroupMemberRepository { get; }
        IGenericRepository<ProfileFollowers> ProfileFollowersRepository { get; }
        IGenericRepository<FriendShip> FriendShipRepository { get; }
        Task<int> SaveAsync(CancellationToken cancellationToken);


        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken);


    }
}
