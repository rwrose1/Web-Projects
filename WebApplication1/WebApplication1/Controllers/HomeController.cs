using NazdaqSearch.Logic.HtmlParser;
using System.Collections.Generic;
using NazdaqSearch.Models;
using System.Web.Mvc;
using NazdaqSearch.Logic.SQLLogic;
using System;
using System.Diagnostics;
using NazdaqSearch.Logic.CSVConversions;
using System.IO;

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

        [HttpPost]
        public ActionResult SymbolDisplay(Inputodel model) {

            List<NazdaqData> display = null;

            String dateString = model.DateInput.ToString("MM/dd/yyyy");

            Debug.WriteLine("\n\n\n" + dateString + "\n\n\n");

            if (model.SymbolInput == null && !dateString.Equals("01/01/0001"))
            {
                display = SQLHelper.GetAllWithDate(dateString);
            }
            else if (model.SymbolInput != null && dateString.Equals("01/01/0001"))
            {
                display = SQLHelper.GetAllWithSymbol(model.SymbolInput);
            } else
            {
                display = SQLHelper.CompareData(SQLHelper.GetAllWithSymbol(model.SymbolInput), dateString);
            }

            NazdaqCSV.dataToCSV(display);

            return View(display);

        }

        public FileResult CsvDownload ()
        {

            var path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/Files/data.csv");

            return File(path, "text/csv", "data.csv");
        }

    }

}