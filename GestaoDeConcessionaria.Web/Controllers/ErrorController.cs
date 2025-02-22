using Microsoft.AspNetCore.Mvc;

namespace GestaoDeConcessionaria.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
