using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ResultPattern;
using MediatR;

namespace Application.Features.Profile.Commands.DeleteProfileCommand
{
    public sealed  record DeleteProfileCommand(int profileId,Guid userId):IRequest<Result>;
 
}
