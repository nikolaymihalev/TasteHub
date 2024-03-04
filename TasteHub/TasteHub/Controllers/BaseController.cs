using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TasteHub.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
