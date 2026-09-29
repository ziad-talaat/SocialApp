using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Voting.Commands.AddVoteCommand
{
    internal class AddVoteValidator:AbstractValidator<AddVoteCommand>
    {
        public AddVoteValidator()
        {
            RuleFor(x=>x.voteDto.VoteType).IsInEnum()
                .WithMessage("VoteType must be one of this list _ Like,Dislike,Support,Sad,Funny,Angry");
        }
    }
}
