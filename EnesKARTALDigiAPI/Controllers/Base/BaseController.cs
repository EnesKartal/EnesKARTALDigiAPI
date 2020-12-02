using EnesKARTALDigiAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnesKARTALDigiAPI.Controllers
{

    [Authorize]
    [ApiController]
    [Produces("application/json")]
    public class BaseController : Controller
    {
        readonly ICacheManager cacheManager;
        public BaseController(
            ICacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        public string Username
        {
            get
            {
                return HttpContext.User.Identity.Name;
            }
        }
    }
}
