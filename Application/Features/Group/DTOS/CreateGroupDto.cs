using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Group.DTOS
{
    public sealed record CreateGroupDto(int?GroupId ,string GroupName,string ?Description,
        string? CoverImageUrl, GroupPrivacy Privacy,
        bool RequireMemberApproval, bool RequirePostApproval)
    {
        public Domain.Entities.Group GetGroup(int ownerId)
        {
            return new Domain.Entities.Group
            {
               GroupName = GroupName,
               Description = Description,
               CoverImageUrl = CoverImageUrl,
               Privacy = Privacy,
               RequireMemberApproval = RequireMemberApproval,
               RequirePostApproval = RequirePostApproval,
               CreatorId=ownerId
            };
        }

        public void UpdateGroup(Domain.Entities.Group group)
        {
            group.GroupName = GroupName;
            group.Description = Description;
            group.CoverImageUrl = CoverImageUrl;
            group.Privacy = Privacy;
            group.RequireMemberApproval = RequireMemberApproval;
            group.RequirePostApproval = RequirePostApproval;
            
        }
    }
}