using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace BudgetExpenditure.Models
{
    public static class DBHeadPropertyMapping
    {
        public static NameValueCollection DBHeadMapping { get; private set; }

        static DBHeadPropertyMapping()
        {
            DBHeadMapping = new NameValueCollection();
            DBHeadMapping.Add("Personnel", "Personell");
            DBHeadMapping.Add("Building Maintenance", "BuildingMaintenance");
            DBHeadMapping.Add("Electricity Expenses", "ElectricityExpenses");
            DBHeadMapping.Add("Water, Security, Fence,Common Utilities, Landscape Maintenance", "CommonUtilities");
            DBHeadMapping.Add("AMC & CMC cost for Assets as per Actuals cost", "AMCCMC");
            DBHeadMapping.Add("Maintenance Rent for Assets (Furniture, Equipments, IT, Other) @24% of Purchase Value for one year", "MaintenanceRent");
            DBHeadMapping.Add("Consumables", "Consumables");
            DBHeadMapping.Add("Stationary (Regular & IT)", "Stationary");
            DBHeadMapping.Add("Medicines and supplies", "MedicinesSupplies");
            DBHeadMapping.Add("Diagnostics", "Diagnostics");
            DBHeadMapping.Add("Transport", "Transport");
            DBHeadMapping.Add("Lodging and Boarding", "LodgingBoarding");
            DBHeadMapping.Add("Remuneration of Community Health Workers", "RemunerationCHW");

            DBHeadMapping.Add("Capacity Building through Training programs, workshops, Exposure Visits", "CapacityBuilding");
            DBHeadMapping.Add("Communication, Publication, Health Literacy & Documentation", "Communication");
            DBHeadMapping.Add("Postage, Telecommunication, Broadband for Teleconsultation & Teletraining and IT", "Postage");
            DBHeadMapping.Add("Labour cost for Events (Surgery Camp, Jatra etc)", "LabourCost");
            DBHeadMapping.Add("Fees / Consultancy for other Services (inhouse / outsourced)", "FeesConsultancy");
            DBHeadMapping.Add("Capital cost: SUB TOTAL", "CapitalCost");
            DBHeadMapping.Add("Organiztional Administrative overheads including Legal / Statuatory Aporovals & Processes (Office space and equipments rent, Communication, salaries of administrative staff, account keeping, audits, bank charges, publicity, public relations, guests, travel, local transport, office stationary, administrative meetings)", "AdministrativeOverheads");
            DBHeadMapping.Add("Contingency fund", "ContingencyFund");
            DBHeadMapping.Add("Income of the Program", "IncomeOftheProgram");
         
        }
    }
}