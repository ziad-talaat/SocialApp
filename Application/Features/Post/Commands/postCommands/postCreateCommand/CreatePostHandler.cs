using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Post.Commands.postCommands.postCreateCommand
{
    internal class CreatePostHandler : IRequestHandler<CreatePostCommand, Guid>
    {
        public Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
