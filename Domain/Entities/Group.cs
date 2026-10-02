using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public sealed  class Group
    {
        public  int Id { get; set; }
        public string GroupName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public  string? CoverImageUrl { get; set; }
        public GroupPrivacy Privacy { get; set; } = GroupPrivacy.Public;
        public bool RequireMemberApproval { get; set; }  
        public bool RequirePostApproval { get; set; }     

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public Profile Creator { get; set; }
        public int CreatorId { get; set; }

        public ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
        public ICollection<Post> Post { get; set; } = new List<Post>();
    }
    public sealed  class GroupMember
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        public GroupRole GroupRole { get; set; }

        public DateTime JoinedDate { get; set; } = DateTime.UtcNow;

    } 

}
