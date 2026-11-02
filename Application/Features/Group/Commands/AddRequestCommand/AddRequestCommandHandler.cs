using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Resources.GlobalResources;
using Application.Resources.GroupResources;
using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Application.Features.Group.Commands.AddRequestCommand
{
    internal sealed class AddRequestCommandHandler(IAppDbContext _context,IUnitOfWork _unitOfWork,ICurrentUser _currentUser,IStringLocalizer<GroupValidationRes>_groupRes, IStringLocalizer<GlobalValidationMessagesRes> _globalRes) : IRequestHandler<AddRequestCommand,Result>
    {
        public async  Task<Result> Handle(AddRequestCommand request, CancellationToken cancellationToken)
        {
          var group= await  _context.Groups.FindAsync(request.groupId,cancellationToken);
            if (group is null)
                return Result.Failure(new Error(_groupRes[GroupValidationRes.GroupNotFound]));

            var profileId = await _currentUser.GetProfileId();
            if (profileId is null)
                return Result.Failure(new Error(_globalRes[GlobalValidationMessagesRes.InvalidOperation]));


              var isRequestExist = await _context.GroupMembers.AnyAsync(x => x.GroupId == request.groupId && x.ProfileId == profileId.Value);

            if (isRequestExist)
                return Result.Failure(new Error(_groupRes[GroupValidationRes.RequestExist]));


            bool canJoinImmediately = !group.RequireMemberApproval && group.Privacy == GroupPrivacy.Public;

            var groupMember = new GroupMember
            {
                GroupId = request.groupId,
                ProfileId = profileId.Value,
                Status=canJoinImmediately?GroupMemberStatus.Active: GroupMemberStatus.Pending,
                JoinedDate= canJoinImmediately ? DateTime.UtcNow:null,
            };
           

                await _unitOfWork.GroupMemberRepository.Insert(groupMember);
            await _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();
        }
    }
}
