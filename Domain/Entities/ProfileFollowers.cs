using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class ProfileFollowers
    {
        public int SourceUserId { get; set; }
        public Profile SourceUser { get; set; }
        public int TargetUserId { get; set; }
        public Profile TargetUser { get; set; }

        public DateTime FollowingDate { get; set; } = DateTime.UtcNow;

    }
}
