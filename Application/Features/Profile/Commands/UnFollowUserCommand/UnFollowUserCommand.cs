using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ResultPattern;
using MediatR;

namespace Application.Features.Profile.Commands.UnFollowUserCommand
{
    public sealed record UnFollowUserCommand(int targetProfileId):IRequest<Result>;


}
