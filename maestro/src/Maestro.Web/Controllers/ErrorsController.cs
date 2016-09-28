using System;
using System.Web.Mvc;
using Maestro.Web.Resources;

namespace Maestro.Web.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return Json(new { ErrorMessage = GlobalStrings.Errors_AnErrorOccurred }, JsonRequestBehavior.AllowGet);
            }

            return View("Error");
        }

        public ActionResult NotFoundError()
        {

            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }


        public ActionResult ErrorMessage(String message)
        {
            if (Request.IsAjaxRequest())
            {
                return Json(new { ErrorMessage = message }, JsonRequestBehavior.AllowGet);
            }

            return View("ErrorMessage");
        }
    }
}