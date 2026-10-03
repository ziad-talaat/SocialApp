using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Localization;
using Serilog;

namespace SocialApp.Controllers
{
   
    public class SettingsController(ILogger<SettingsController> _logger,IDiagnosticContext diagonosticContext) : BaseController
    {

        //[HttpPost("set-language")]
        //public IActionResult SetCulture(string culture)
        //{

        //}

        [HttpPost("set-language")]
        public IActionResult SetCulture()
        {

            string name = "mohamed"; 

            _logger.LogDebug("this is for ->>>>>>>>>>>>> {UserName}",name);

            diagonosticContext.Set("Data", "????????????????Hi\"\"\"\"\"\"\"\"\"\"\"\"");  // after added app.UseSerilogRequestLogging(); middleware  after build of project  it will add last final log per each request containg baic info about the request  the IDiagonosticContext gives a way to manipulate or to add extra info to that log 


            return Ok();

        }
    }
}
