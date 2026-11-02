using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IAppDbContext
    {
         DbSet<AppUser> AppUsers { get;}
         DbSet<Profile> Profiles { get;}
         DbSet<Post> Posts { get;}
         DbSet<Vote> Votes { get;  }
         DbSet<PostVote> PostVotes { get;  }
         DbSet<PostImages> PostImages { get;  }
         DbSet<Comment> Comments { get;  }
         DbSet<Group> Groups { get; }
         DbSet<GroupMember> GroupMembers { get; }
         DbSet<ProfileFollowers> ProfileFollowers { get; }
         DbSet<FriendShip> FriendShips { get;  }
    }
}
