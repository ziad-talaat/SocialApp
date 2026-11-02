using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public sealed  class FriendShip
    {
        public int SenderId { get; set; }
        public Profile Sender { get; set; } = null!;

        public int ReceiverId { get; set; }
        public Profile Receiver { get; set; } = null!;

        public DateTime? CreationDate { get; set; } = null;

        public FriendshipStatus FriendShipStatus { get; set; }=FriendshipStatus.Pending;


    }
}
