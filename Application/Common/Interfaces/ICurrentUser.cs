using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public  interface ICurrentUser
    {
        Guid? UserId { get; }
        Task<int?> GetProfileId();


    }
}
