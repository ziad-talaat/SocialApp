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
    internal sealed class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.ProfileName).IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.Profiles)
                   .HasForeignKey(x => x.UserId).IsRequired();
            
        }
    }
}
