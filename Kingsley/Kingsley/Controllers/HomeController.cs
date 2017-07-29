using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;

namespace Kingsley.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Analyze(string text)
        {
            var textBody = text;
            var textList = textBody.Split(' ').ToList();
            var results = from t in textList
                group t by t
                into textGroup
                orderby textGroup.Key descending
                select new
                {
                    Word = textGroup.Key,
                    Count = textGroup.Count()
                };

            int uniqueWords = (from tGroup in results
                where tGroup.Count == 1
                select tGroup).Count();

            var finalResults = new ResultModel (uniqueWords, textList.Count, results);

            ViewData["TotalWords"] = finalResults.TotalWords;
            ViewData["UniqueWords"] = finalResults.UniqueWords;
            ViewData["Results"] = finalResults.Results;

            return RedirectToAction("Index");
        }
    }

    public class ResultModel
    {
        public int UniqueWords { get; set; }
        public int TotalWords { get; set; }
        public IEnumerable<object> Results { get; set; }

        public ResultModel(int uniqueWords, int totalWords, IEnumerable<object> results)
        {
            UniqueWords = uniqueWords;
            TotalWords = totalWords;
            Results = results;
        }
    }
}