using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
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
           var profile=await  _context.Profiles.FindAsync(request.profileDto?.ProfileId);
            if (profile == null)
                return Result.Failure(new Error("no such profile"));

            if (profile.UserId != request.userId)
                return Result.Failure(new Error("forbidden Action"));

            profile.ProfileName = request.profileDto.ProfileName;
            profile.Bio=request.profileDto.Bio;
            profile.BackgroundImage = request.profileDto.BackgroundImageUrl;
            profile.ImageUrl= request.profileDto.ImageUrl;
            await _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();
        }
    }
}
