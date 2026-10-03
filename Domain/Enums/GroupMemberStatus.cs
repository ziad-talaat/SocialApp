

namespace Domain.Enums
{
    public enum GroupMemberStatus
    {
        Pending = 0, Active = 1, Invited = 2, Banned = 3
    }
}

// pending   -> when user send request to join private groups
// active  -> when admin or owner accept user order or user join public group
//invited -> when admin or owner send invitation to user
// banned ->  when admin or owner banned a user 
