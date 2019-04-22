using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetExpenditure.Models
{
    public class BudgetExpenditureViewModel
    {
        string cNames = "user0,user1,user2,user3,user4";
        string rNames = "a,b,c,d,e";



        public HeadDepartmentTable mytable { get; set; }
        
        public BudgetExpenditureViewModel()
        {
            mytable = new HeadDepartmentTable(cNames.Split(",".ToCharArray()),
            rNames.Split(",".ToCharArray()));
        }
       
    }
}