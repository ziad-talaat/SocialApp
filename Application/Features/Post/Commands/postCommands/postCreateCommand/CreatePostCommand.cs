using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MediatR;

namespace Application.Features.Post.Commands.postCommands.postCreateCommand
{
    public sealed record CreatePostCommand(string title):IRequest<Guid>;
}
