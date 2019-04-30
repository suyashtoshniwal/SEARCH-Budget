//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BudgetExpenditure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BudgetExpenditure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BudgetExpenditure()
        {
            this.EstimatedBudget = 0m;
        }
    
        public int Id { get; set; }
        public string Year { get; set; }
        public int HeadId { get; set; }
        public int DepartmentId { get; set; }
        public Nullable<decimal> EstimatedBudget { get; set; }
        public Nullable<decimal> ExpenditureQ1 { get; set; }
        public Nullable<decimal> ExpenditureQ2 { get; set; }
        public Nullable<decimal> ExpenditureQ3 { get; set; }
        public Nullable<decimal> ExpenditureQ4 { get; set; }
        public Nullable<decimal> TotalExpenditure { get; set; }
        public Nullable<decimal> PlannedLastYear { get; set; }
        public Nullable<decimal> ActualLastYear { get; set; }

        public string Quarter { get; set; }
        public Dictionary<int, string> Departments { get; set; }

        public Dictionary<int, string> Heads { get; set; }

        public virtual Head Head { get; set; }
        public virtual Department Department { get; set; }
    }
}
