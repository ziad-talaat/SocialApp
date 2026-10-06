using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ResultPattern;
using Application.Features.Group.DTOS;
using MediatR;

namespace Application.Features.Group.Commands.CreateGroupCommand
{
    public sealed record  CreateGroupCommand(CreateGroupDto createGroupDto):IRequest<Result>;
  
}
