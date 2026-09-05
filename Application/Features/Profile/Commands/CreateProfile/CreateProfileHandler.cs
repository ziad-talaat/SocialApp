using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Features.Authentication.Commands.Register;
using Application.Features.Authentication.DTOS;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace Application.Features.Profile.Commands.CreateProfile
{
    internal sealed class CreateProfileHandler(IAppDbContext _context,IUnitOfWork _unitOfWork) : IRequestHandler<CreateProfileCommand, Result>
    {
        public async Task<Result> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var count= _context.Profiles.Where(x=>x.UserId==request.userId).Count();
            if (count >= 3)
                return Result.Failure(new Error("you have maximum count of accounts"));

            Domain.Entities.Profile profile = new Domain.Entities.Profile
            {
                ImageUrl = request.profileDto.ImageUrl,
                ProfileName = request.profileDto.ProfileName,
                Description = request.profileDto.Description,
                UserId = request.userId
            };
           await _unitOfWork.ProfileRepository.Insert(profile);
           await  _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();
        }
    }
}
