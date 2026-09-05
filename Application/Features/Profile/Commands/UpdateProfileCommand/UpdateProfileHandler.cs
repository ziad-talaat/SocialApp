using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace Application.Features.Profile.Commands.UpdateProfileCommand
{
    internal sealed class UpdateProfileHandler(IAppDbContext _context,IUnitOfWork _unitOfWork) : IRequestHandler<UpdateProfileCommand, Result>
    {
        public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
           var profile=await  _context.Profiles.FindAsync(request.profileDto.ProfileId);
            if (profile == null)
                return Result.Failure(new Error("no such profile"));


            profile.ProfileName = request.profileDto.ProfileName;
            profile.Description=request.profileDto.Description;
            profile.BackgroundImage = request.profileDto.BackgroundImageUrl;
            profile.ImageUrl= request.profileDto.ImageUrl;  
        }
    }
}
