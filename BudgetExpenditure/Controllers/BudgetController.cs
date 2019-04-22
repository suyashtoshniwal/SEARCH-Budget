using BudgetExpenditure.Models;
using BudgetExpenditure.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetExpenditure.Controllers
{
    public class BudgetController : Controller
    {
        BudgetExpenditureEntities budgetEntities = new BudgetExpenditureEntities();
        // GET: Budget
        public ActionResult Index()
        {
            //string cNames = "user0,user1,user2,user3,user4";
            //string rNames = "a,b,c,d,e";
            //BudgetExpenditureViewModel viewModel = new BudgetExpenditureViewModel();


            //viewModel.mytable["user1", "b"] = 1;
            //viewModel.mytable["user1", "c"] = 2;
            //viewModel.mytable["user0", "a"] = 3;
            //viewModel.mytable["user2", "d"] = 4;
            //viewModel.mytable["user3", "e"] = 5;
            //viewModel.mytable["user4", "a"] = 6;
            //viewModel.mytable["user0", "b"] = 7;

            var nirmanEntities = new BudgetExpenditure.Models.BudgetExpenditureEntities();
            var context = nirmanEntities.BudgetExpenditures;
            var departments = nirmanEntities.Departments.ToDictionary(m => m.Id, m => m.Name);
            var heads = nirmanEntities.Heads.ToDictionary(m => m.Id, m => m.Name);
            //List<string> selectedColumns = new List<string>();

            //foreach(var selectedColumn in SelectedColumnFields)
            //{
            //    int selectedColumnInt = Convert.ToInt32(selectedColumn);

            //    selectedColumns.Add(Enum.GetName(typeof(ColumnEnumerations.Columns), selectedColumnInt));
            //}

            //IEnumerable<string> newList = selectedColumns;
            //ViewBag.SelectedColumns = newList;

            var participants = context.ToList();
            participants[0].Departments = departments;
            participants[0].Heads = heads;

            return View("ReportPivotBudget", participants);

            
        }

        public ActionResult Create()
        {
            var model = new HeadEntry();

            var departments = budgetEntities.Departments.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });


            model.Departments = departments;

            var next25Years = from n in Enumerable.Range(0, 25)
                             select DateTime.Now.Year + n;

            model.Years = next25Years;

            var years = next25Years.Select(c => new SelectListItem
            {
                Value = c.ToString() + "-" + ((int)c + 1).ToString() ,
                Text = c.ToString() + "-" + ((int)c + 1).ToString()
            });

            model.Years1 = years;


            return View(model);
        }

        [HttpPost]
        public ActionResult Create(HeadEntry head)
        {

            // Convert to the actual model

            Models.BudgetExpenditure budgetExpenditure = new Models.BudgetExpenditure();

            budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            if (!string.IsNullOrEmpty(head.Personell.ToString()))
            {
                budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Personnel").FirstOrDefault().Id;
                budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                budgetExpenditure.Year = head.Year;
                budgetExpenditure.EstimatedBudget = head.Personell;
                budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                
            }

            if (!string.IsNullOrEmpty(head.BuildingMaintenance.ToString()))
            {
                budgetExpenditure = new Models.BudgetExpenditure();
                budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Building Maintenance").FirstOrDefault().Id;
                budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                budgetExpenditure.Year = head.Year;
                budgetExpenditure.EstimatedBudget = head.BuildingMaintenance;
                budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            }
            if (!string.IsNullOrEmpty(head.CapacityBuilding.ToString()))
            {
                budgetExpenditure = new Models.BudgetExpenditure();
                budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Capacity Building through Training programs, workshops, Exposure Visits").FirstOrDefault().Id;
                budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                budgetExpenditure.Year = head.Year;
                budgetExpenditure.EstimatedBudget = head.CapacityBuilding;
                budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            }


            //if (!string.IsNullOrEmpty(head.Personell.ToString()))
            //{
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Personnel").FirstOrDefault().Id;
            //    budgetExpenditure.EstimatedBudget = head.Personell;
            //}
            //if (!string.IsNullOrEmpty(head.Personell.ToString()))
            //{
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Personnel").FirstOrDefault().Id;
            //    budgetExpenditure.EstimatedBudget = head.Personell;
            //}
            //if (!string.IsNullOrEmpty(head.Personell.ToString()))
            //{
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Personnel").FirstOrDefault().Id;
            //    budgetExpenditure.EstimatedBudget = head.Personell;
            //}
            //if (!string.IsNullOrEmpty(head.Personell.ToString()))
            //{
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Personnel").FirstOrDefault().Id;
            //    budgetExpenditure.EstimatedBudget = head.Personell;
            //}
            //if (!string.IsNullOrEmpty(head.Personell.ToString()))
            //{
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Personnel").FirstOrDefault().Id;
            //    budgetExpenditure.EstimatedBudget = head.Personell;
            //}
            //if (!string.IsNullOrEmpty(head.Personell.ToString()))
            //{
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Personnel").FirstOrDefault().Id;
            //    budgetExpenditure.EstimatedBudget = head.Personell;
            //}
            //if (!string.IsNullOrEmpty(head.Personell.ToString()))
            //{
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Personnel").FirstOrDefault().Id;
            //    budgetExpenditure.EstimatedBudget = head.Personell;
            //}

            //var model = new HeadEntry();

            //var departments = budgetEntities.Departments.Select(c => new SelectListItem
            //{
            //    Value = c.Id.ToString(),
            //    Text = c.Name
            //});


            //model.Departments = departments;

            try
            {
                budgetEntities.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }

            return View("Index");
        }
    }
}