using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Resources;
using Application.Resources.GlobalResources;
using Application.Resources.GroupResources;
using Domain.Enums;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Application.Features.Group.Commands.UpdateGroupCommand
{
    internal sealed class UpdateGroupCommandHandler(IAppDbContext _context,IUnitOfWork _unitOfWork,ICurrentUser _currentUser,IStringLocalizer<GlobalValidationMessagesRes> _globalValidationRes, IStringLocalizer<GroupValidationRes> _groupValidationRes) : IRequestHandler<UpdateGroupCommand, Result>
    {
        public async Task<Result> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.UserId.HasValue)
                return Result.Failure(new Error(_globalValidationRes[nameof(GlobalValidationMessagesRes.UnAuthorizedUser)]));


          var group=await  _context.Groups.FindAsync(request.updateGroupDto.GroupId);
            if(group is null)
                return Result.Failure(new Error(_groupValidationRes[nameof(GroupValidationRes.GroupNotFound)]));


          var profileId= await  _currentUser.GetProfileId();
            if (profileId is null)
                return Result.Failure(new Error(_globalValidationRes[nameof(GlobalValidationMessagesRes.UnExistProfile)]));


            var groupMember = await  _context.GroupMembers.FirstOrDefaultAsync(x => x.GroupId == group.Id && x.ProfileId == profileId.Value);
            if(groupMember is null || groupMember.GroupRole ==GroupRole.Member)
            {
                return Result.Failure(new Error(_groupValidationRes[nameof(GroupValidationRes.PermissionDenied)]));

            }

               request.updateGroupDto.UpdateGroup(group);

            await _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();
        }
    }
}
