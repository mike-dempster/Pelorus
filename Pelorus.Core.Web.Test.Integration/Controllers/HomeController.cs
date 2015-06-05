using System;
using System.Web.Mvc;

namespace Pelorus.Core.Web.Test.Integration.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return this.View("Index");
        }

        [HttpPost]
        public ActionResult Throw(string message)
        {
            throw new Exception(message);
        }
    }
}
