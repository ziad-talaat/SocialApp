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
    internal sealed class FriendShipConfiguration : IEntityTypeConfiguration<FriendShip>
    {
        public void Configure(EntityTypeBuilder<FriendShip> builder)
        {
            builder.HasKey(x => new {x.SenderId, x.ReceiverId});
            builder.HasIndex(x => x.ReceiverId);

            builder.HasOne(x=>x.Sender).WithMany(x=> x.SentFRiendShipRequests)
                .HasForeignKey(x => x.SenderId).IsRequired().OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Receiver).WithMany(x => x.ReceivedFRiendShipRequests)
              .HasForeignKey(x => x.ReceiverId).IsRequired().OnDelete(DeleteBehavior.Restrict);

        }
    }
}
