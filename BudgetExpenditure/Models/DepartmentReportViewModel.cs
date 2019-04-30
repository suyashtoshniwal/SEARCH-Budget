using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetExpenditure.Models
{
    public class DepartmentReportViewModel
    {
        public string Head { get; set; }

        public string Department { get; set; }

        public decimal EstimatedBudget { get; set; }

        public decimal ActualExpenditureTillQuarter { get; set; }

        public decimal BalanceLeftTillQuarter { get; set; }

        public decimal ExpenditureQ1 { get; set; }

        public decimal ExpenditureQ2 { get; set; }

        public decimal ExpenditureQ3 { get; set; }

        public decimal ExpenditureQ4 { get; set; }

        public decimal BalanceLeft { get; set; }
    }
}