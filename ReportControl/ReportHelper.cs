using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Web.Mvc
{
    public static class ReportHelper
    {
        //public static DataTable Pivot<T>(this IEnumerable<T> source, string rowField, string dataField, AggregateFunction aggregate, params string[] columnFields) where T : class
        //{
        //    DataTable dt = source.ToDataTable();
        //    return dt.Pivot(rowField, dataField, aggregate, columnFields);
        //}

        //public static DataTable Pivot(this DataTable source, string rowField, string dataField, AggregateFunction aggregate, params string[] columnFields)
        //{
        //    Pivot p = new Pivot(source);
        //    return p.PivotData(rowField, dataField, aggregate, columnFields);
        //}

        public static Table ToTable(this DataTable dt, IEnumerable<string> columns, IEnumerable<string> itemTemplates)
        {
            Table table = new Table();
            TableRow row = new TableRow();

            foreach (string col in columns)
                row.Cells.Add(new TableHeaderCell() { Text = col });
            row.TableSection = TableRowSection.TableHeader;
            table.Rows.Add(row);

            foreach (DataRow dr in dt.Rows)
            {
                row = new TableRow();
                foreach (object o in dr.ItemArray)
                    row.Cells.Add(new TableCell { Text = o.ToString() });
                table.Rows.Add(row);
            }

            return table;
        }

        public static void AddIndex(this Table table)
        {
            int index = 1;
            foreach (TableRow row in table.Rows)
            {
                if (row.TableSection == TableRowSection.TableHeader)
                    row.Cells.AddAt(0, new TableCell() { Text = "S. No." });
                else
                    row.Cells.AddAt(0, new TableCell { Text = index++ + "." });
            }
        }

        public static void PivotHeader(this Table table, string separator, int pivotLevel)
        {
            TableRow row = table.Rows[0];
            if (row.TableSection == TableRowSection.TableHeader)
            {
                TableRow r = new TableRow();
                var headers = row.Cells.Cast<TableCell>().Select(x => x.Text);

                for (int i = 0; i < pivotLevel; i++)
                {
                    r = new TableRow();
                    r.TableSection = TableRowSection.TableHeader;
                    foreach (var x in headers)
                    {
                        string headerText = GetNthText(x, i, separator);
                        if (r.Cells.Count > 0 && r.Cells[r.Cells.Count - 1].Text == headerText)
                            r.Cells[r.Cells.Count - 1].ColumnSpan++;
                        else
                            r.Cells.Add(new TableHeaderCell { Text = headerText, ColumnSpan = 1 });
                    }
                    table.Rows.AddAt(i, r);
                }
            }
            table.Rows.Remove(row);
        }

        private static string GetNthText(string input, int n, string separator)
        {
            string[] texts = input.Split(separator.ToCharArray(), StringSplitOptions.None);
            return texts.Length > n ? texts[n] : string.Empty;
        }

        public static HtmlString ToHtmlString(this Table table)
        {
            StringWriter sw = new StringWriter();
            table.RenderControl(new HtmlTextWriter(sw));
            return new HtmlString(sw.ToString());
        }
    }
}
