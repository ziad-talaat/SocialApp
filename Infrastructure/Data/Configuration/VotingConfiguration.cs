using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Data.Configuration
{
    internal sealed class VotingConfiguration : IEntityTypeConfiguration<PostVote>
    {
        public void Configure(EntityTypeBuilder<PostVote> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.HasIndex(x => new { x.ProfileId, x.PostId })
            .IsUnique();
            builder.HasOne(x => x.Profile).WithMany(x => x.PostVotes)
                .HasForeignKey(x => x.ProfileId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Post).WithMany(x => x.PostVotes)
                .HasForeignKey(x => x.PostId).IsRequired().OnDelete(DeleteBehavior.NoAction); 
        }
    }
}
