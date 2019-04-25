using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetExpenditure.Models
{
    public class DepartmentReportViewModel
    {
        public string Head { get; set; }

        public string EstimatedBudget { get; set; }

        public string ActualExpenditureTillQuarter { get; set; }

        public string BalanceLeftTillQuarter { get; set; }

        public string ExpenditureQ1 { get; set; }

        public string ExpenditureQ2 { get; set; }

        public string ExpenditureQ3 { get; set; }

        public string ExpenditureQ4 { get; set; }

        public string BalanceLeft { get; set; }
    }
}