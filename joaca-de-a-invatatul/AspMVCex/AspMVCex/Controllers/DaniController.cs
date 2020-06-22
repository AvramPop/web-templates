using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspMVCex.Controllers
{
    public class DaniController : Controller
    {
        // GET: Dani
        public ActionResult Index()
        {
            return View("ViewDani");
        }
    }
}