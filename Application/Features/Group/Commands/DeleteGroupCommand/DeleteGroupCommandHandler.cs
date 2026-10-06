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

namespace Application.Features.Group.Commands.DeleteGroupCommand
{
    internal sealed class DeleteGroupCommandHandler(IAppDbContext _context,IUnitOfWork _unitOfWork,IStringLocalizer<GroupValidationRes>_groupValidationRes) : IRequestHandler<DeleteGroupCommand,Result>
    {
        public  async Task<Result> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
          var group=  await _context.Groups.FindAsync(request.groupId);
            if (group is null)
                return Result.Failure(new Error(_groupValidationRes[nameof(GroupValidationRes.GroupNotFound)]));

            _unitOfWork.GroupRepository.Delete(group);
           await  _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();    

        }
    }
}
