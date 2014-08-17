using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReCaptchaDemo.Models;

namespace ReCaptchaDemo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetRecaptchaImage()
        {

            string re = "GenerateRecaptcha.ashx?RcId=" + Guid.NewGuid();
            return Json(re);
        }
        public JsonResult GetRecaptchaImage2()
        {

            string re = "GenerateRecaptcha2.ashx?RcId=" + Guid.NewGuid();
            return Json(re);
        }
        [HttpPost]
        public ActionResult ValidateRecaptcha(Employee employee)
        {
            string msg;
            if (HttpContext.Session != null && Convert.ToString(HttpContext.Session["captcha"]) == employee.ReCaptchaCode)
            {
                msg = "Recaptcha Validation Success";
            }
            else
            {
                msg = "Recaptcha Validation Fail";
            }
            ViewBag.RechaptchaMessage1 = msg;
            return View("Index");
        }
        [HttpPost]
        public ActionResult ValidateRecaptcha2(Employee employee)
        {
            string msg;
            if (HttpContext.Session != null &&
                Convert.ToString(HttpContext.Session["captcha2"]) == employee.ReCaptchaCode)
            {
                msg = "Recaptcha Validation Success";
            }
            else
            {
                msg = "Recaptcha Validation Fail";
            }
            ViewBag.RechaptchaMessage2 = msg;
            return View("Index");
        }

    }
}
