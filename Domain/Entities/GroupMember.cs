using Domain.Enums;

namespace Domain.Entities
{
    public sealed  class GroupMember
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        public GroupRole GroupRole { get; set; }
        public GroupMemberStatus Status { get; set; } = GroupMemberStatus.Active;
        public DateTime JoinedDate { get; set; } = DateTime.UtcNow;

    } 

}
