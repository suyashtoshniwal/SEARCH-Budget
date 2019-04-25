using BudgetExpenditure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetExpenditure.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report

        BudgetExpenditureEntities budgetEntities = new BudgetExpenditureEntities();

        public ActionResult Index()
        {

            // Get department from login

            var currentYear = DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString();
            var result = budgetEntities.BudgetExpenditures.Where(c => c.Year == currentYear && c.DepartmentId == 1).ToList();
            List<DepartmentReportViewModel> departmentViewModels = new List<DepartmentReportViewModel>();
            foreach(var budgetEntity in result)
            {
                var departmentReportViewModel = new DepartmentReportViewModel();
                departmentReportViewModel.Head = budgetEntities.Heads.Where(c => c.Id == budgetEntity.HeadId).FirstOrDefault().Name;
                departmentReportViewModel.EstimatedBudget = budgetEntity.EstimatedBudget.ToString();
                departmentReportViewModel.ActualExpenditureTillQuarter = budgetEntity.ExpenditureQ1.ToString();
                departmentReportViewModel.BalanceLeftTillQuarter = budgetEntity.TotalExpenditure.ToString();
                departmentViewModels.Add(departmentReportViewModel);
            }

            return View("DepartmentReport", departmentViewModels);
        }



    }
}