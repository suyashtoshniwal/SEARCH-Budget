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

            var quarters = new List<String>()
            {
                "Q1","Q2","Q3","Q4"
            };

            var quartersSelectList = quarters.Select(c => new SelectListItem
            {
                Value = c.ToString(),
                Text = c.ToString()
            });
            model.Quarters = quartersSelectList;

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
                if (record == null)
                {                                    
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Personnel").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.Personell;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.Personell;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.Personell;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.Personell;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.BuildingMaintenance.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                 c.Year == head.Year &&
                                                                 c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Building Maintenance").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Building Maintenance").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.BuildingMaintenance;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.BuildingMaintenance;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.BuildingMaintenance;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.BuildingMaintenance;
                        break;
                }
            }

            
            if (!string.IsNullOrEmpty(head.ElectricityExpenses.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Electricity Expenses").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Electricity Expenses").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.ElectricityExpenses;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.ElectricityExpenses;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.ElectricityExpenses;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.ElectricityExpenses;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.CommonUtilities.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Water, Security, Fence,Common Utilities, Landscape Maintenance").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Water, Security, Fence,Common Utilities, Landscape Maintenance").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.CommonUtilities;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.CommonUtilities;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.CommonUtilities;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.CommonUtilities;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.AMCCMC.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "AMC & CMC cost for Assets as per Actuals cost").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
               {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "AMC & CMC cost for Assets as per Actuals cost").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.AMCCMC;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.AMCCMC;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.AMCCMC;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.AMCCMC;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.MaintenanceRent.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Maintenance Rent for Assets (Furniture, Equipments, IT, Other) @24% of Purchase Value for one year").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
               {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Maintenance Rent for Assets (Furniture, Equipments, IT, Other) @24% of Purchase Value for one year").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.MaintenanceRent;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.MaintenanceRent;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.MaintenanceRent;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.MaintenanceRent;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.Consumables.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Consumables").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Consumables").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.Consumables;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.Consumables;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.Consumables;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.Consumables;
                        break;
                }
            }


            if (!string.IsNullOrEmpty(head.Stationary.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Stationary (Regular & IT)").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Stationary (Regular & IT)").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.Stationary;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.Stationary;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.Stationary;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.Stationary;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.MedicinesSupplies.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Medicines and supplies").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Medicines and supplies").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.MedicinesSupplies;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.MedicinesSupplies;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.MedicinesSupplies;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.MedicinesSupplies;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.Diagnostics.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Diagnostics").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Diagnostics").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.Diagnostics;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.Diagnostics;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.Diagnostics;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.Diagnostics;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.Transport.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Transport").FirstOrDefault().Id).FirstOrDefault();
                if (record != null)
                { 
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Transport").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.Transport;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.Transport;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.Transport;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.Transport;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.LodgingBoarding.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Lodging and Boarding").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Lodging and Boarding").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.LodgingBoarding;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.LodgingBoarding;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.LodgingBoarding;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.LodgingBoarding;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.RemunerationCHW.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Remuneration of Community Health Workers").FirstOrDefault().Id).FirstOrDefault();
                if (record != null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Remuneration of Community Health Workers").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.RemunerationCHW;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.RemunerationCHW;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.RemunerationCHW;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.RemunerationCHW;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.CapacityBuilding.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Capacity Building through Training programs, workshops, Exposure Visits").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Capacity Building through Training programs, workshops, Exposure Visits").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.CapacityBuilding;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.CapacityBuilding;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.CapacityBuilding;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.CapacityBuilding;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.Communication.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Communication, Publication, Health Literacy & Documentation").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                   budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Communication, Publication, Health Literacy & Documentation").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.Communication;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.Communication;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.Communication;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.Communication;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.Postage.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Postage, Telecommunication, Broadband for Teleconsultation & Teletraining and IT").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Postage, Telecommunication, Broadband for Teleconsultation & Teletraining and IT").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.Postage;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.Postage;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.Postage;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.Postage;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.LabourCost.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Labour cost for Events (Surgery Camp, Jatra etc)").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Labour cost for Events (Surgery Camp, Jatra etc)").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.LabourCost;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.LabourCost;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.LabourCost;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.LabourCost;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.FeesConsultancy.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Fees / Consultancy for other Services (inhouse / outsourced)").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Fees / Consultancy for other Services (inhouse / outsourced)").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.FeesConsultancy;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.FeesConsultancy;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.FeesConsultancy;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.FeesConsultancy;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.CapitalCost.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Capital cost: SUB TOTAL").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Capital cost: SUB TOTAL").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.CapitalCost;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.CapitalCost;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.CapitalCost;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.CapitalCost;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.TotalExpenditure.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Total Expenditure (A + B + C + D)").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Total Expenditure (A + B + C + D)").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.TotalExpenditure;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.TotalExpenditure;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.TotalExpenditure;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.TotalExpenditure;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.AdministrativeOverheads.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Organiztional Administrative overheads including Legal / Statuatory Aporovals & Processes (Office space and equipments rent, Communication, salaries of administrative staff, account keeping, audits, bank charges, publicity, public relations, guests, travel, local transport, office stationary, administrative meetings)").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Organiztional Administrative overheads including Legal / Statuatory Aporovals & Processes (Office space and equipments rent, Communication, salaries of administrative staff, account keeping, audits, bank charges, publicity, public relations, guests, travel, local transport, office stationary, administrative meetings)").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.AdministrativeOverheads;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.AdministrativeOverheads;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.AdministrativeOverheads;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.AdministrativeOverheads;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.ContingencyFund.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Contingency fund").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Contingency fund").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.ContingencyFund;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.ContingencyFund;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.ContingencyFund;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.ContingencyFund;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.GrandTotalExpenditure.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Grand Total Expenditure (E + F + G)").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Grand Total Expenditure (E + F + G)").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.GrandTotalExpenditure;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.GrandTotalExpenditure;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.GrandTotalExpenditure;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.GrandTotalExpenditure;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.IncomeOftheProgram.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Income of the Program").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Income of the Program").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.IncomeOftheProgram;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.IncomeOftheProgram;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.IncomeOftheProgram;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.IncomeOftheProgram;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(head.TotalBudgetaryRequirement.ToString()))
            {
                var record = budgetEntities.BudgetExpenditures.Where(c => c.DepartmentId == head.CurrentDepartmentId &&
                                                                c.Year == head.Year &&
                                                                c.HeadId == budgetEntities.Heads.Where(h => h.Name == "Total Budgetary Requirement (H - I)").FirstOrDefault().Id).FirstOrDefault();
                if (record == null)
                {
                    budgetExpenditure.HeadId = budgetEntities.Heads.Where(c => c.Name == "Total Budgetary Requirement (H - I)").FirstOrDefault().Id;
                    budgetExpenditure.DepartmentId = head.CurrentDepartmentId;
                    budgetExpenditure.Year = head.Year;
                    budgetEntities.BudgetExpenditures.Add(budgetExpenditure);
                }
                switch (head.CurrentQuarterName)
                {
                    case "Q1":
                        record.ExpenditureQ1 = head.TotalBudgetaryRequirement;
                        break;
                    case "Q2":
                        record.ExpenditureQ2 = head.TotalBudgetaryRequirement;
                        break;
                    case "Q3":
                        record.ExpenditureQ3 = head.TotalBudgetaryRequirement;
                        break;
                    case "Q4":
                        record.ExpenditureQ4 = head.TotalBudgetaryRequirement;
                        break;
                }
            }

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