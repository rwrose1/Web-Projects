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

            if (model.SymbolInput == null && model.DateInput != null)
            {

                String dateString = model.DateInput.ToString("MM/dd/yyyy");

                Debug.WriteLine("\n\n\n" + dateString + "\n\n\n");
            

                display = SQLHelper.GetAllWithDate(dateString);
            }
            else if (model.SymbolInput != null && model.DateInput == null)
            {
                display = SQLHelper.GetAllWithSymbol(model.SymbolInput);
            } else
            {
                String dateString = model.DateInput.ToString().Substring(0, model.DateInput.ToString().IndexOf(' '));

                display = SQLHelper.CompareData(SQLHelper.GetAllWithSymbol(model.SymbolInput), dateString);
            }


            return View(display);

        }

        public FileResult CsvDownload (List<NazdaqData> model)
        {
            NazdaqCSV.dataToCSV(model);

            return File("me.txt", "test/plain", "NazdaqSearch/Files");
        }

    }

}