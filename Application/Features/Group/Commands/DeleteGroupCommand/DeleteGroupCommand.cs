using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ResultPattern;
using MediatR;

namespace Application.Features.Group.Commands.DeleteGroupCommand
{
    public sealed record DeleteGroupCommand(int groupId):IRequest<Result>;
    
    
}
