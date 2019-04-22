using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetExpenditure.Controllers
{
    public class ExpenditureController : Controller
    {
        // GET: Expenditure
        public ActionResult Index()
        {
            return View();
        }
    }
}