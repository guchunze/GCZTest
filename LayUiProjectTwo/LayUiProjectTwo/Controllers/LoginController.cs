using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayUiProjectTwo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public object PostLoginInfo(dynamic obj)
        {
            Dictionary<string, object> results = null;
            return results;
        }

    }
}