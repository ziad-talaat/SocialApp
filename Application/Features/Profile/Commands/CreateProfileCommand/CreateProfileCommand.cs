using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ResultPattern;
using Application.Features.Profile.DTOS;
using MediatR;

namespace Application.Features.Profile.Commands.CreateProfile
{
    public sealed record CreateProfileCommand(ProfileDto profileDto,Guid userId):IRequest<Result>;
   
}
