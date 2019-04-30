using BudgetExpenditure.Models;
using BudgetExpenditure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
            foreach (var budgetEntity in result)
            {
                var departmentReportViewModel = new DepartmentReportViewModel();
                departmentReportViewModel.Head = budgetEntities.Heads.Where(c => c.Id == budgetEntity.HeadId).FirstOrDefault().Name;
                departmentReportViewModel.EstimatedBudget = budgetEntity.EstimatedBudget.Value;
                departmentReportViewModel.ActualExpenditureTillQuarter = budgetEntity.ExpenditureQ1.Value;
                departmentReportViewModel.BalanceLeftTillQuarter = budgetEntity.TotalExpenditure.Value;
                departmentViewModels.Add(departmentReportViewModel);
            }

            return View("DepartmentReport", departmentViewModels);
        }


        public ActionResult DrawDepartmentwiseHeadwiseChart(int? departmentId, int? headId)
        {
            // if departmentid is null then get department of logged in user
            if (departmentId == null)
            {
                departmentId = 1;
            }
            if (headId == null)
            {
                headId = 1;
            }
            var currentYear = DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString();
            var result = budgetEntities.BudgetExpenditures.Where(c => c.Year == currentYear && c.DepartmentId == departmentId).ToList();
            var headResult = result.Where(c => c.HeadId == headId).FirstOrDefault();

            var departmentReportViewModel = new DepartmentReportViewModel();
            departmentReportViewModel.Head = budgetEntities.Heads.Where(c => c.Id == 1).FirstOrDefault().Name;
            departmentReportViewModel.EstimatedBudget = headResult.EstimatedBudget.Value;
            departmentReportViewModel.ActualExpenditureTillQuarter = headResult.ExpenditureQ1.Value;
            departmentReportViewModel.BalanceLeftTillQuarter = headResult.TotalExpenditure.Value;
            departmentReportViewModel.ExpenditureQ1 = headResult.ExpenditureQ1.Value;
            departmentReportViewModel.ExpenditureQ2 = headResult.ExpenditureQ1.Value + headResult.ExpenditureQ2.Value;
            departmentReportViewModel.ExpenditureQ3 = headResult.ExpenditureQ1.Value + headResult.ExpenditureQ2.Value + headResult.ExpenditureQ3.Value;
            departmentReportViewModel.ExpenditureQ4 = headResult.ExpenditureQ1.Value + headResult.ExpenditureQ2.Value + headResult.ExpenditureQ3.Value + headResult.ExpenditureQ4.Value;


            //var departmentReportViewModel = new DepartmentReportViewModel();
            ////departmentReportViewModel.Head = budgetEntities.Heads.Where(c => c.Id == 1).FirstOrDefault().Name;
            //departmentReportViewModel.EstimatedBudget = 40000;
            //departmentReportViewModel.ActualExpenditureTillQuarter = 1000;
            //departmentReportViewModel.BalanceLeftTillQuarter = 2000;
            //departmentReportViewModel.ExpenditureQ1 = 1000;
            //departmentReportViewModel.ExpenditureQ2 = 2000;
            //departmentReportViewModel.ExpenditureQ3 = 4000;
            //departmentReportViewModel.ExpenditureQ4 = 5000;

            return View("DepartmentwiseHeadwiseChart", departmentReportViewModel);
        }

        public ActionResult DrawHeadwiseAllDepartmentsChart(int? headId)
        {
            if (headId == null)
            {
                headId = 1;
            }

            var currentYear = DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString();
            var result = budgetEntities.BudgetExpenditures.Where(c => c.Year == currentYear && c.HeadId == headId).GroupBy(c => c.HeadId)
                .Select(lg => new DepartmentReportViewModel()
                {
                    EstimatedBudget = lg.Sum(w => w.EstimatedBudget.Value),
                    ExpenditureQ1 = lg.Sum(w => w.ExpenditureQ1.Value),
                    ExpenditureQ2 = lg.Sum(w => w.ExpenditureQ2.Value) + lg.Sum(w => w.ExpenditureQ1.Value),
                    ExpenditureQ3 = lg.Sum(w => w.ExpenditureQ3.Value) + lg.Sum(w => w.ExpenditureQ2.Value) + lg.Sum(w => w.ExpenditureQ1.Value),
                    ExpenditureQ4 = lg.Sum(w => w.ExpenditureQ4.Value) + lg.Sum(w => w.ExpenditureQ3.Value) + lg.Sum(w => w.ExpenditureQ2.Value) + lg.Sum(w => w.ExpenditureQ1.Value)
                }).ToList();




            //var departmentReportViewModel = new DepartmentReportViewModel();
            //departmentReportViewModel.Head = budgetEntities.Heads.Where(c => c.Id == 1).FirstOrDefault().Name;
            //departmentReportViewModel.EstimatedBudget = headResult.EstimatedBudget.Value;
            //departmentReportViewModel.ActualExpenditureTillQuarter = headResult.ExpenditureQ1.Value;
            //departmentReportViewModel.BalanceLeftTillQuarter = headResult.TotalExpenditure.Value;
            //departmentReportViewModel.ExpenditureQ1 = headResult.ExpenditureQ1.Value;
            //departmentReportViewModel.ExpenditureQ2 = headResult.ExpenditureQ1.Value + headResult.ExpenditureQ2.Value;
            //departmentReportViewModel.ExpenditureQ3 = headResult.ExpenditureQ1.Value + headResult.ExpenditureQ2.Value + headResult.ExpenditureQ3.Value;
            //departmentReportViewModel.ExpenditureQ4 = headResult.ExpenditureQ1.Value + headResult.ExpenditureQ2.Value + headResult.ExpenditureQ3.Value + headResult.ExpenditureQ4.Value;


            //var departmentReportViewModel = new DepartmentReportViewModel();
            ////departmentReportViewModel.Head = budgetEntities.Heads.Where(c => c.Id == 1).FirstOrDefault().Name;
            //departmentReportViewModel.EstimatedBudget = 40000;
            //departmentReportViewModel.ActualExpenditureTillQuarter = 1000;
            //departmentReportViewModel.BalanceLeftTillQuarter = 2000;
            //departmentReportViewModel.ExpenditureQ1 = 1000;
            //departmentReportViewModel.ExpenditureQ2 = 2000;
            //departmentReportViewModel.ExpenditureQ3 = 4000;
            //departmentReportViewModel.ExpenditureQ4 = 5000;






            return View("HeadwiseAllDepartmentsChart", result);
        }


        public ActionResult Create()
        {
            var model = new HeadEntry();

            var departments = budgetEntities.Departments.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });

            departments.ToList().Add(new SelectListItem() { Value = budgetEntities.Departments.Count().ToString(), Text = "All Departments" });

            model.Departments = departments;

            var next25Years = from n in Enumerable.Range(0, 25)
                              select DateTime.Now.Year + n;

            model.Years = next25Years;

            var years = next25Years.Select(c => new SelectListItem
            {
                Value = c.ToString() + "-" + ((int)c + 1).ToString(),
                Text = c.ToString() + "-" + ((int)c + 1).ToString()
            });

            model.Years1 = years;

           
            var heads = budgetEntities.Heads.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });

            model.Heads = heads;

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateChart(HeadEntry head)
        {
            var currentDepartment = budgetEntities.Departments.Where(c => c.Id == head.CurrentDepartmentId).Select(c => c.Name);

            if (currentDepartment.ToList()[0] == "All Departments")
            {

                return DrawHeadwiseAllDepartmentsChart(head.CurrentHeadId);
            }
            else
            {
                return DrawDepartmentwiseHeadwiseChart(head.CurrentDepartmentId, head.CurrentHeadId);
            }
        }

        [HttpPost]
        public ActionResult CreateTable(HeadEntry head)
        {
            var currentDepartment = budgetEntities.Departments.Where(c => c.Id == head.CurrentDepartmentId).Select(c => c.Name);


            var currentYear = DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString();
            var result = budgetEntities.BudgetExpenditures.Where(c => c.Year == currentYear && c.DepartmentId == head.CurrentDepartmentId).ToList();
            List<DepartmentReportViewModel> departmentViewModels = new List<DepartmentReportViewModel>();
            foreach (var budgetEntity in result)
            {
                var departmentReportViewModel = new DepartmentReportViewModel();
                departmentReportViewModel.Department = budgetEntities.Departments.Where(c => c.Id == head.CurrentDepartmentId).Select(c => c.Name).SingleOrDefault();
                departmentReportViewModel.Head = budgetEntities.Heads.Where(c => c.Id == budgetEntity.HeadId).FirstOrDefault().Name;
                departmentReportViewModel.EstimatedBudget = budgetEntity.EstimatedBudget.Value;
                departmentReportViewModel.ActualExpenditureTillQuarter = budgetEntity.ExpenditureQ1.Value;
                departmentReportViewModel.BalanceLeftTillQuarter = budgetEntity.TotalExpenditure.Value;
                departmentViewModels.Add(departmentReportViewModel);
            }

            return View("DepartmentalAllHeadsTabularReport", departmentViewModels);

        }

        }
}