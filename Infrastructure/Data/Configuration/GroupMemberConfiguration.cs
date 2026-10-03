using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Data.Configuration
{
    internal sealed class GroupMemberConfiguration : IEntityTypeConfiguration<GroupMember>
    {
        public void Configure(EntityTypeBuilder<GroupMember> builder)
        {
            builder.HasKey(x => new { x.ProfileId, x.GroupId });
            builder.HasIndex(x => x.GroupId); 

            builder.HasOne(x=>x.Group).WithMany(x=>x.GroupMembers)
                .HasForeignKey(x=>x.GroupId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Profile).WithMany(x => x.GroupMemberships)
               .HasForeignKey(x => x.ProfileId).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}