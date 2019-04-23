using BudgetExpenditure.Models;
using BudgetExpenditure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetExpenditure.Controllers
{
    public class Budget1Controller : Controller
    {

        BudgetExpenditureEntities budgetEntities = new BudgetExpenditureEntities();

        // GET: Budget1
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            var model = new HeadEntry();
            var next25Years = from n in Enumerable.Range(0, 25)
                              select DateTime.Now.Year + n;

            var departments = budgetEntities.Departments.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });

            model.Years = next25Years;

            var years = next25Years.Select(c => new SelectListItem
            {
                Value = c.ToString() + "-" + ((int)c + 1).ToString(),
                Text = c.ToString() + "-" + ((int)c + 1).ToString()
            });

            model.Years1 = years;
            model.Year = (DateTime.Now.Year).ToString() + "-" + (DateTime.Now.Year + 1).ToString();

            model.Departments = departments;
            model.CurrentDepartmentId = budgetEntities.Departments.FirstOrDefault().Id;
           // model.CurrentDepartmentId = budgetEntities.Departments.Where(c => c.Name == "Tribal Health").First().Id;
            var selected = model.Departments.Where(x => x.Value == model.CurrentDepartmentId.ToString()).First();
            selected.Selected = true;

            var budgetEntitiesResult = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == model.CurrentDepartmentId && c.Year == model.Year).ToList();

            foreach(var budgetEntityResult in budgetEntitiesResult)
            {
                // Get head
                var head = budgetEntityResult.Head.Name;
                model[DBHeadPropertyMapping.DBHeadMapping[head]] = budgetEntityResult.EstimatedBudget;
            }

            return View("Create", model);
        }
    }
}