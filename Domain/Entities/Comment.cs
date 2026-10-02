using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Comment
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public bool  IsUpdated { get; set; }
        //public  AppUser User { get; set; }
        //public Guid UserId { get; set; }

        public Profile Profile { get; set; }
        public int  ProfileId{ get; set; }

        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}
