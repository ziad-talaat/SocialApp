using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ResultPattern;
using Application.Features.Profile.DTOS;
using MediatR;

namespace Application.Features.Profile.Commands.UpdateProfileCommand
{
    public sealed  record UpdateProfileCommand(ProfileDto profileDto,Guid userId):IRequest<Result>;
    
    
}
