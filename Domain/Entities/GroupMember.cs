using Domain.Enums;

namespace Domain.Entities
{
    public sealed  class GroupMember
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        public GroupRole GroupRole { get; set; } = GroupRole.Member;
        public GroupMemberStatus Status { get; set; } = GroupMemberStatus.Pending;
        public DateTime? JoinedDate { get; set; } = null;
    } 

}
