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
        BudgetExpenditureEntities budgetEntities  = new BudgetExpenditureEntities();
        // GET: Budget
        public ActionResult Index()
        {
            

            var budgetEntities = new BudgetExpenditure.Models.BudgetExpenditureEntities();
            var context = budgetEntities.BudgetExpenditures;
            var departments = budgetEntities.Departments.ToDictionary(m => m.Id, m => m.Name);
            var heads = budgetEntities.Heads.ToDictionary(m => m.Id, m => m.Name);
           

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

            if (!string.IsNullOrEmpty(head.AdministrativeOverheads.ToString()))
            {
                budgetExpenditure = new Models.BudgetExpenditure();
                budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Organizational Administrative overheads including Legal / Statuatory Aporovals & Processes (Office space and equipments rent, Communication, salaries of administrative staff, account keeping, audits, bank charges, publicity, public relations, guests, travel, local transport, office stationary, administrative meetings)").FirstOrDefault().Id;
                budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                budgetExpenditure.Year = head.Year;
                budgetExpenditure.EstimatedBudget = head.AdministrativeOverheads;
                budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            }

            if (!string.IsNullOrEmpty(head.AMCCMC.ToString()))
            {
                budgetExpenditure = new Models.BudgetExpenditure();
                budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "AMC & CMC cost for Assets as per Actuals cost").FirstOrDefault().Id;
                budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                budgetExpenditure.Year = head.Year;
                budgetExpenditure.EstimatedBudget = head.AdministrativeOverheads;
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