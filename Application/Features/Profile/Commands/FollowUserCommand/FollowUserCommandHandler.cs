using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Resources.GlobalResources;
using Application.Resources.ProfileResources;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Application.Features.Profile.Commands.FollowUserCommand
{
    internal sealed class FollowUserCommandHandler(IAppDbContext _context,
        IUnitOfWork _unitOfWork,ICurrentUser _currentUser,IStringLocalizer<GlobalValidationMessagesRes>_globalRes, IStringLocalizer<ProfileValidationResource> _profileRes)
        : IRequestHandler<FollowUserCommand, Result>
    {
        public async Task<Result> Handle(FollowUserCommand request, CancellationToken cancellationToken)
        {
            var sourceId = await _currentUser.GetProfileId();
            if(!sourceId.HasValue)
                return Result.Failure(new Error(_globalRes[GlobalValidationMessagesRes.UnAuthenticatedUser]));
           
            if(sourceId.Value==request.targetProfileId)
                return Result.Failure(new Error(_profileRes[ProfileValidationResource.CycleProfileFollow]));


            var targetExist = await _context.Profiles.AnyAsync(x=>x.Id==request.targetProfileId,cancellationToken);
            if(!targetExist)
                return Result.Failure(new Error(_profileRes[ProfileValidationResource.unExistProfile]));


            var existFolow=await _context.ProfileFollowers.AnyAsync(x=>x.SourceUserId==sourceId.Value && x.TargetUserId==request.targetProfileId, cancellationToken);
            if(existFolow)
                return Result.Failure(new Error(_profileRes[ProfileValidationResource.alreadyFollowed]));

            var profileFollow=new ProfileFollowers
            {
                SourceUserId = sourceId.Value,
                TargetUserId = request.targetProfileId
            };

           await _unitOfWork.ProfileFollowersRepository.Insert(profileFollow);
            await _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();
        }
    }
}
