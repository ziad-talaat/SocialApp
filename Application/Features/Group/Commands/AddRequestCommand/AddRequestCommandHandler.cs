using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Resources.GroupResources;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.Group.Commands.AddRequestCommand
{
    internal sealed class AddRequestCommandHandler(IAppDbContext _context,IUnitOfWork _unitOfWork,IStringLocalizer<GroupValidationRes>_groupRes) : IRequestHandler<AddRequestCommand,Result>
    {
        public async  Task<Result> Handle(AddRequestCommand request, CancellationToken cancellationToken)
        {
          var group= await  _context.Groups.FindAsync(request.groupId,cancellationToken);
            if (group is null)
                return Result.Failure(new Error(nameof(_groupRes[GroupValidationRes.GroupNotFound])));


        }
    }
}
