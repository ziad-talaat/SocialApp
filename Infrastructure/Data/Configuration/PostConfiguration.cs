using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Data.Configuration
{
    internal sealed class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
          builder.HasKey(x=>x.Id);
            builder.HasOne(x => x.Profile).WithMany(x => x.Posts)
                .HasForeignKey(x => x.ProfileId).IsRequired();
            builder.HasMany(x => x.PostImages).WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId).IsRequired();

            builder.HasOne(x => x.Profile).WithMany(x => x.Posts)
                .HasForeignKey(x => x.ProfileId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
