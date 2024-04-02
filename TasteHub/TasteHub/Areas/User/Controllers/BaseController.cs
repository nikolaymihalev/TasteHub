using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TasteHub.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class BaseController : Controller
    {
    }
}
