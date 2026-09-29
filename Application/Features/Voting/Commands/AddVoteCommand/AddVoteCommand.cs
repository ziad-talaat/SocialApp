using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ResultPattern;
using Application.Features.Voting.DTOS;
using MediatR;

namespace Application.Features.Voting.Commands.AddVoteCommand
{
    public sealed record AddVoteCommand(VoteDTO voteDto):IRequest<Result>;
}
