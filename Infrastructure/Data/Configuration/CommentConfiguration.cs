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
    internal sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.CreationDate).IsRequired();

            builder.HasOne(x=>x.Profile).WithMany(x=>x.Comments).HasForeignKey(x=>x.ProfileId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Post).WithMany(x => x.Comments).HasForeignKey(x => x.PostId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
