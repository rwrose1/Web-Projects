using NazdaqSearch.Logic.HtmlParser;
using System.Collections.Generic;
using NazdaqSearch.Models;
using NazdaqSearch.Logic.NazdaqCSV;
using System.Web.Mvc;

namespace NazdaqSearch.Controllers
{
 
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
        
            return View();

        }

        public ViewResult Contact() 
        {

            return View();

        }

        public ActionResult Display() {

            List<NazdaqData> display = HtmlParsing.getData();

            //NazdaqCSV.dataToCSV(display);

            return View(display);

        }
    
    }

}