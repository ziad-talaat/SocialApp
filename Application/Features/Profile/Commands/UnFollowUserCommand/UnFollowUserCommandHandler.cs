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

namespace Application.Features.Profile.Commands.UnFollowUserCommand
{
    internal sealed class UnFollowUserCommandHandler(IAppDbContext _context,
        IUnitOfWork _unitOfWork, ICurrentUser _currentUser, IStringLocalizer<GlobalValidationMessagesRes> _globalRes, IStringLocalizer<ProfileValidationResource> _profileRes) : IRequestHandler<UnFollowUserCommand, Result>
    {
        public async Task<Result> Handle(UnFollowUserCommand request, CancellationToken cancellationToken)
        {
            var sourceId = await _currentUser.GetProfileId();
            if (!sourceId.HasValue)
                return Result.Failure(new Error(_globalRes[GlobalValidationMessagesRes.UnAuthenticatedUser]));

            if (sourceId.Value == request.targetProfileId)
                return Result.Failure(new Error(_globalRes[GlobalValidationMessagesRes.InvalidOperation]));


            var targetExist = await _context.Profiles.AnyAsync(x => x.Id == request.targetProfileId, cancellationToken);
            if (!targetExist)
                return Result.Failure(new Error(_profileRes[ProfileValidationResource.unExistProfile]));


            var follow = await _context.ProfileFollowers.SingleOrDefaultAsync(x => x.SourceUserId == sourceId.Value && x.TargetUserId == request.targetProfileId, cancellationToken);
            if (follow is null)
                return Result.Failure(new Error(_profileRes[ProfileValidationResource.UnExistFollow]));

           

             _unitOfWork.ProfileFollowersRepository.Delete(follow);
            await _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();
        }
    }
}
