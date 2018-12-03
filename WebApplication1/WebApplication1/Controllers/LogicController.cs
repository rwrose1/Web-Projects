using NazdaqSearch.Logic.HtmlParser;
using System.Collections.Generic;
using NazdaqSearch.Models;
using System.Web.Mvc;
using NazdaqSearch.Logic.SQLLogic;

namespace WebApplication1.Controllers
{
    public class LogicController : Controller
    {

        public ActionResult Refresh()
        {
            List<NazdaqData> toInsert = HtmlParsing.getData();

            foreach(NazdaqData item in toInsert)
            {
                SQLHelper.Insert(item);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}