using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindRussianWords.Models;

namespace FindRussianWords.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        
        
        public ActionResult Index()
        {
            return View();
        }

       /* public ActionResult DoItMothefucker()
        {
            StringModel.ReadFromFile();
            return RedirectToAction("Index");
        }*/

        public ActionResult GoXml()
        {
            StringModel.FileNapoln();
            return RedirectToAction("Index");
        }

        public ActionResult GoReadAndReplaceXml()
        {
            StringModel.ReadFromXml();
            return RedirectToAction("Index");
        }

        public ActionResult TestController()
        {
            ForTest foreTest = new ForTest();
            foreTest.StringResultForTest = "some test";
            return View(foreTest);

        }
        [HttpPost]
        public ActionResult TestController(ForTest textBoxHell)
        {

            textBoxHell.OperationWithString();
            return View(textBoxHell);

        }


    }
}
