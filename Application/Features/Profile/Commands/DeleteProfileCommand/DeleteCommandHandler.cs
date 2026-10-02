using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Domain.IRepositories;
using MediatR;

namespace Application.Features.Profile.Commands.DeleteProfileCommand
{
    internal sealed class DeleteCommandHandler(IAppDbContext _context,ICurrentUser _currentUser,IUnitOfWork _unitOfWork) : IRequestHandler<DeleteProfileCommand, Result>
    {
        public async  Task<Result> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
        {
            if(!_currentUser.UserId.HasValue)
                return Result.Failure(new Error("Forbidden Operation"));

            var profile = await _context.Profiles.FindAsync(request.profileId);
            if (profile == null || profile.UserId!=_currentUser.UserId)
                return Result.Failure(new Error("No such Profile"));

            _unitOfWork.ProfileRepository.Delete(profile);
           await  _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();    
        }
    }
}
