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
        public DbSet<Vote> Votes { get;  }
        public DbSet<PostVote> PostVotes { get;  }
    }
}
