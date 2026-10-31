using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Resources.GlobalResources;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Application.Features.Profile.Commands.LockAccountCommand
{
    internal sealed class LockUnLockAccountCommandHandler(IAppDbContext _context,IUnitOfWork _unitOfWork,IStringLocalizer<GlobalValidationMessagesRes>_globalMessagesRes) : IRequestHandler<LockUnLockAccountCommand, Result>
    {
        public async Task<Result> Handle(LockUnLockAccountCommand request, CancellationToken cancellationToken)
        {
           var profile=await  _context.Profiles.AsTracking().SingleOrDefaultAsync(x=>x.Id==request.profileId);
            if (profile is null)
                return Result.Failure(new Error(_globalMessagesRes[nameof(GlobalValidationMessagesRes.UnExistProfile)]));

            profile.IsLocked=!profile.IsLocked;
             await   _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();
        }
    }
}
