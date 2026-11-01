using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Resources.GlobalResources;
using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
namespace Application.Features.Group.Commands.CreateGroupCommand
{
    internal sealed class CreateGroupCommandHandler(ICurrentUser _currentUser,IStringLocalizer<GlobalValidationMessagesRes>_globalValidationRes, IAppDbContext _context,IUnitOfWork _unitOfWork) : IRequestHandler<CreateGroupCommand, Result>
    {
        public async Task<Result> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.UserId.HasValue)
                return Result.Failure(new Error(_globalValidationRes[nameof(GlobalValidationMessagesRes.UnAuthorizedUser)]));

            var profile= await _context.Profiles.FirstOrDefaultAsync(x=>x.UserId==_currentUser.UserId.Value);
            if(profile is null)
                return Result.Failure(new Error(_globalValidationRes[nameof(GlobalValidationMessagesRes.UnExistProfile)]));

            Domain.Entities.Group group =request.createGroupDto.GetGroup(profile.Id);


           await  _unitOfWork.GroupRepository.Insert(group);
            var groupMember = new GroupMember
            {
                GroupId = group.Id,
                ProfileId = profile.Id,
                GroupRole = GroupRole.Owner,
                Status = GroupMemberStatus.Active,
                JoinedDate = DateTime.UtcNow
            };
            await _unitOfWork.GroupMemberRepository.Insert(groupMember);
            await _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();
        }
    }
}
