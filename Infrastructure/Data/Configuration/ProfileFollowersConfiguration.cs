using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    internal sealed class ProfileFollowersConfiguration : IEntityTypeConfiguration<ProfileFollowers>
    {
        public void Configure(EntityTypeBuilder<ProfileFollowers> builder)
        {
            builder.HasKey(x => new {x.SourceUserId,x.TargetUserId });

            builder.HasOne(x=>x.SourceUser).WithMany(x=>x.Following).HasForeignKey(x => x.SourceUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.TargetUser).WithMany(x => x.Followers).HasForeignKey(x => x.TargetUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.TargetUserId);
        }
    }
}
