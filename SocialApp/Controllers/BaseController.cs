using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        protected Guid? GetLoggedUserId()
        {
            var result = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid id);
            if (!result)
                return null;
            return id;
        }
    }
}
