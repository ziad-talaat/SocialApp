using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ResultPattern;
using MediatR;

namespace Application.Features.Profile.Commands.AcceptFriendRequest
{
    public sealed record AcceptFriendRequestCommand(int sourceId):IRequest<Result>;

}
