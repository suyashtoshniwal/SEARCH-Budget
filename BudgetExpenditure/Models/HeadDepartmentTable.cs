using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetExpenditure.Models
{
    public class HeadDepartmentTable
    {
        private string[] _columns;
        private string[] _rows;
        private int[,] _table;

        public HeadDepartmentTable(string[] colnames, string[] rownames)
        {
            this._columns = colnames;
            this._rows = rownames;
            this._table = new int[colnames.Length, rownames.Length];
        }

        public int this[string colName, string rowName]
        {
            get
            {
                int i = Array.IndexOf(_columns, colName);
                int j = Array.IndexOf(_rows, rowName);
                if (i != -1 && j != -1)
                {
                    return _table[i, j];
                }
                else
                {
                    throw new System.IndexOutOfRangeException();
                }
            }
            set
            {
                int i = Array.IndexOf(_columns, colName);
                int j = Array.IndexOf(_rows, rowName);
                if (i != -1 && j != -1)
                {
                    _table[i, j] = value;
                }
                else
                {
                    throw new System.IndexOutOfRangeException();
                }
            }
        }
    }
}