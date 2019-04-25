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
    public class ExpenditureController : Controller
    {
        BudgetExpenditureEntities budgetEntities = new BudgetExpenditureEntities();
        // GET: Expenditure
        public ActionResult Index()
        {
            var budgetEntities = new BudgetExpenditure.Models.BudgetExpenditureEntities();
            var context = budgetEntities.BudgetExpenditures;
            var departments = budgetEntities.Departments.ToDictionary(m => m.Id, m => m.Name);
            var heads = budgetEntities.Heads.ToDictionary(m => m.Id, m => m.Name);


            var participants = context.Where(c => c.DepartmentId == 1 && c.Year == "2019-2020").ToList();

            participants[0].Departments = departments;
            participants[0].Heads = heads;

            return View("ReportPivotExpenditure", participants);
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
                Value = c.ToString() + "-" + ((int)c + 1).ToString(),
                Text = c.ToString() + "-" + ((int)c + 1).ToString()
            });

            model.Years1 = years;


            return View("Q1Create", model);
        }


        [HttpPost]
        public ActionResult Create(HeadEntry head)
        {

            // Convert to the actual model

            Models.BudgetExpenditure budgetExpenditure = new Models.BudgetExpenditure();

            budgetExpenditure.DepartmentId = head.CurrentDepartmentId;


            // Get the record for dept,head,year

            if (!string.IsNullOrEmpty(head.Personell.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                 c.Year == head.Year &&
                                                                 c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Personnel").FirstOrDefault().Id).FirstOrDefault();
                if (record != null)
                {
                    record.ExpenditureQ1 = head.Personell;
                }
                else
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Personnel").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetExpenditure.ExpenditureQ1 = head.Personell;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
            }

            if (!string.IsNullOrEmpty(head.BuildingMaintenance.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Building Maintenance").FirstOrDefault().Id).First();
                if (record != null)
                {
                    record.ExpenditureQ1 = head.Personell;
                }
                else
                {
                    budgetExpenditure = new Models.BudgetExpenditure();
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Building Maintenance").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetExpenditure.ExpenditureQ1 = head.BuildingMaintenance;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
            }

            //if (!string.IsNullOrEmpty(head.ElectricityExpenses.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Electricity Expenses").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.ElectricityExpenses;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.CommonUtilities.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Water, Security, Fence,Common Utilities, Landscape Maintenance").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.CommonUtilities;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.AMCCMC.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "AMC & CMC cost for Assets as per Actuals cost").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.AMCCMC;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.MaintenanceRent.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Maintenance Rent for Assets (Furniture, Equipments, IT, Other) @24% of Purchase Value for one year").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.MaintenanceRent;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.Consumables.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Consumables").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.Consumables;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.Stationary.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Stationary (Regular & IT)").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.Stationary;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.MedicinesSupplies.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Medicines and supplies").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.MedicinesSupplies;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.Diagnostics.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Diagnostics").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.Diagnostics;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.Transport.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Transport").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.Transport;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.LodgingBoarding.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Lodging and Boarding").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.LodgingBoarding;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.RemunerationCHW.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Remuneration of Community Health Workers").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.RemunerationCHW;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.CapacityBuilding.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Capacity Building through Training programs, workshops, Exposure Visits").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.CapacityBuilding;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.Communication.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Communication, Publication, Health Literacy & Documentation").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.Communication;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.Postage.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Postage, Telecommunication, Broadband for Teleconsultation & Teletraining and IT").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.Postage;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.LabourCost.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Labour cost for Events (Surgery Camp, Jatra etc)").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.LabourCost;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.FeesConsultancy.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Fees / Consultancy for other Services (inhouse / outsourced)").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.FeesConsultancy;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.CapitalCost.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Capital cost: SUB TOTAL").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.CapitalCost;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.TotalExpenditure.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Total Expenditure (A + B + C + D)").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.TotalExpenditure;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.AdministrativeOverheads.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Organiztional Administrative overheads including Legal / Statuatory Aporovals & Processes (Office space and equipments rent, Communication, salaries of administrative staff, account keeping, audits, bank charges, publicity, public relations, guests, travel, local transport, office stationary, administrative meetings)").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.AdministrativeOverheads;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.ContingencyFund.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Contingency fund").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.ContingencyFund;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.GrandTotalExpenditure.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Grand Total Expenditure (E + F + G)").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.GrandTotalExpenditure;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.IncomeOftheProgram.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Income of the Program").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.IncomeOftheProgram;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}

            //if (!string.IsNullOrEmpty(head.TotalBudgetaryRequirement.ToString()))
            //{
            //    budgetExpenditure = new Models.BudgetExpenditure();
            //    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Total Budgetary Requirement (H - I)").FirstOrDefault().Id;
            //    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
            //    budgetExpenditure.Year = head.Year;
            //    budgetExpenditure.ExpenditureQ1 = head.TotalBudgetaryRequirement;
            //    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
            //}



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