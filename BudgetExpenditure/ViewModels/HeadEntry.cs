using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;

namespace BudgetExpenditure.ViewModels
{
    public class HeadEntry
    {
        public string Year { get; set; }

        public string Department { get; set; }

        public int CurrentDepartmentId { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }

        public IEnumerable<int> Years { get; set; }

        public IEnumerable<SelectListItem> Years1 { get; set; }

        public decimal Personell { get; set; }

        public decimal BuildingMaintenance { get; set; }

        public decimal ElectricityExpenses { get; set; }

        public decimal CommonUtilities { get; set; }
        
        public decimal AMCCMC { get; set; }

        public decimal MaintenanceRent { get; set; }

        public decimal Consumables { get; set; }

        public decimal Stationary { get; set; }

        public decimal MedicinesSupplies { get; set; }

        public decimal Diagnostics { get; set; }

        public decimal Transport { get; set; }

        public decimal LodgingBoarding { get; set; }

        public decimal RemunerationCHW { get; set; }

        public decimal CapacityBuilding { get; set; }

        public decimal Communication { get; set; }

        public decimal Postage { get; set; }

        public decimal LabourCost { get; set; }

        public decimal FeesConsultancy { get; set; }

        public decimal CapitalCost { get; set; }
        public decimal AdministrativeOverheads { get; set; }
        public decimal ContingencyFund { get; set; }
        public decimal IncomeOftheProgram { get; set; }

    }
}