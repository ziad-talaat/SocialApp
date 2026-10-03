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
    internal class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(x => x.GroupName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(10000);
            builder.Property(x => x.CreationDate).IsRequired();




            builder.HasIndex(x => x.GroupName);





            builder.HasOne(x=>x.Creator).WithMany(x=>x.OwnedGroups)
                .HasForeignKey(x=>x.CreatorId).IsRequired().OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Post).WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId).IsRequired(false).OnDelete(DeleteBehavior.Cascade); 



        }
    }
}
