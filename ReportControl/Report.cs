using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace System.Web.Mvc
{
    public static class ReportEx
    {
        private static string _Separator = ".";
        public static HtmlString Report<T>(this IEnumerable<T> source, params ReportColumn[] reportColumns) where T : class
        {
            return source.Report(false, reportColumns.Select(x => x.HeaderText), reportColumns.Select(x => x.TemplateItem), reportColumns.Select(x => x.style));
        }

        public static HtmlString Report<T>(this IEnumerable<T> source, bool showIndex, params ReportColumn[] reportColumns) where T : class
        {
            Table table = new Table();
            table.CellSpacing = 0;
            TableRow row;
            TableCell cell;

            List<string> props = typeof(T).GetProperties().Select(x => x.Name).ToList();

            row = new TableRow();
            foreach (string col in reportColumns.Select(x => x.HeaderText))
                row.Cells.Add(new TableHeaderCell() { Text = col });
            row.TableSection = TableRowSection.TableHeader;
            table.Rows.Add(row);

            foreach (var item in source)
            {
                row = new TableRow();
                foreach (ReportColumn col in reportColumns)
                {
                    cell = new TableCell();
                    cell.Text = props.Contains(col.TemplateItem) ? GetData<T>(item, col.TemplateItem) : ParseData(item, props, col.TemplateItem);
                    if (col.style != null)
                        cell.ApplyStyle(col.style);
                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
            }

            if (showIndex)
                table.AddIndex();

            var tableHtmlString = table.ToHtmlString().ToString();
            return table.ToHtmlString();
            //return source.Report(false, reportColumns.Select(x => x.HeaderText), reportColumns.Select(x => x.TemplateItem), reportColumns.Select(x => x.style));
        }
        public static HtmlString Report(this DataTable source, params string[] columns)
        {
            return source.Report(string.Empty, 1, columns);
        }
        public static HtmlString Report(this DataTable source, string cssClass, params string[] columns)
        {
            return source.Report(cssClass, 1, columns);
        }
        private static HtmlString Report(this DataTable source, string cssClass, int pivotLevel, params string[] columns)
        {
            if (columns == null || columns.Length == 0)
                columns = source.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            List<string> colList = new List<string>();

            List<string> headers = columns.Select(x => x.Split(new string[] { "::" }, StringSplitOptions.None).First()).ToList();
            List<string> itemTemplates = columns.Select(x => x.Split(new string[] { "::" }, StringSplitOptions.None).Last()).ToList();

            if (itemTemplates.Last().Equals("..."))
            {
                List<string> tailCol = source.Columns.Cast<DataColumn>().Where(x => !itemTemplates.Contains(x.ColumnName)).Select(x => x.ColumnName).ToList();
                headers.Remove("...");
                itemTemplates.Remove("...");
                headers.AddRange(tailCol);
                itemTemplates.AddRange(tailCol);
                //colList.AddRange(source.Columns.Cast<DataColumn>().Where(x=> x.ColumnName).Select(x => x.ColumnName));
            }
            Table table = source.ToTable(headers, itemTemplates);
            table.CellSpacing = 0;
            table.CssClass = cssClass;
            table.PivotHeader(".", pivotLevel);

            table.ApplyStyle(new UI.WebControls.Style() { CssClass = "table table-striped table-bordered table-condensed" });

            var tableHtmlString = table.ToHtmlString().ToString();

            return table.ToHtmlString();
        }
        public static HtmlString Report<T>(this IEnumerable<T> source, params string[] columns) where T : class
        {
            return source.Report(false, columns);
        }
        public static HtmlString Report<T>(this IEnumerable<T> source, bool showIndex, params string[] columns) where T : class
        {
            IEnumerable<string> headers = columns.Select(x => x.Split(new string[] { "::" }, StringSplitOptions.None).First());
            IEnumerable<string> itemTemplates = columns.Select(x => x.Split(new string[] { "::" }, StringSplitOptions.None).Last());
            return source.Report(showIndex, headers, itemTemplates);
        }
        public static HtmlString Report<T>(this IEnumerable<T> source, bool showIndex, IEnumerable<string> headers, IEnumerable<string> itemTemplates, IEnumerable<Style> styles = null) where T : class
        {
            Table table = new Table();
            table.CellSpacing = 0;
            TableRow row;
            TableCell cell;

            List<string> props = typeof(T).GetProperties().Select(x => x.Name).ToList();

            row = new TableRow();
            foreach (string col in headers)
                row.Cells.Add(new TableHeaderCell() { Text = col });
            row.TableSection = TableRowSection.TableHeader;
            table.Rows.Add(row);

            foreach (var item in source)
            {
                row = new TableRow();
                for (int colIndex = 0; colIndex < itemTemplates.Count(); colIndex++)  // in itemTemplates)
                {
                    string col = itemTemplates.ElementAt(colIndex);
                    cell = new TableCell();
                    cell.Text = props.Contains(col) ? GetData<T>(item, col) : ParseData(item, props, col);
                    if (styles != null)
                        cell.ApplyStyle(styles.ElementAt(colIndex));
                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
            }

            if (showIndex)
                table.AddIndex();

            var tableHtmlString = table.ToHtmlString().ToString();

            return table.ToHtmlString();
        }

        private static string GetData<T>(T item, string col) where T : class
        {
            return item.GetType().GetProperty(col).GetValue(item, null).ToString();
        }

        //public static HtmlString ReportWithPivot<T>(this IEnumerable<T> source, string cssClass, string rowField, string dataField, AggregateFunction aggregate, IEnumerable<string> columnFields) where T : class
        //{
        //    DataTable dt = source.ToDataTable();
        //    return dt.ReportWithPivot(cssClass, rowField, dataField, aggregate, columnFields);
        //}

        public static HtmlString ReportWithPivot<T>(this IEnumerable<T> source, string cssClass, string rowField, string dataField, AggregateFunction aggregate, Dictionary<int, string> departments, Dictionary<int, string> heads, params string[] columnFields) where T : class
        {
            DataTable dt = source.ToDataTable();
            return dt.ReportWithPivot(cssClass, rowField, dataField, aggregate,departments, heads, columnFields);
        }

        public static HtmlString ReportWithPivot(this DataTable source, string cssClass, string rowField, string dataField, AggregateFunction aggregate, Dictionary<int, string> departments, Dictionary<int, string> heads, IEnumerable<string> columnFields)
        {
            Pivot p = new Pivot(source);
            DataTable dt = p.PivotData(rowField, dataField, aggregate,departments, heads, columnFields);
            return dt.Report(cssClass, columnFields.ToList().Count(), dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray());
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            //PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            PropertyInfo[] properties = typeof(T).GetProperties();
            DataTable table = new DataTable();
            foreach (PropertyInfo prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyInfo prop in properties)
                    row[prop.Name] = prop.GetValue(item, null) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        private static string ParseData(object item, List<string> props, string expression)
        {
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            //pairs.AddRange(props.Select(x=> new { x, "" + x + ""}));

            foreach (string prop in props)
            {
                string head = "##" + prop + "##";
                if (expression.Contains(head))
                    expression = expression.Replace(head, item.GetType().GetProperty(prop).GetValue(item, null).ToString());
            }
            return expression;
        }
        private static string CreateHeader(IEnumerable<string> headerTexts, int pivotLevel)
        {
            string[][] headers = headerTexts.Select(x => x.Split(_Separator.ToCharArray(), StringSplitOptions.None)).ToArray();

            StringBuilder sb = new StringBuilder();
            sb.Append("<thead><tr>");
            //for (int level = 0; level < pivotLevel; level++)
            {
                foreach (string[] s in headers)
                {
                    for (int index = 0; index < s.Length; index++)
                        sb.AppendFormat("<th>{0}</th>", s[index]);
                }
            }
            sb.Append("</tr></thead>");
            return sb.ToString();
        }


        private static string GetHeaderText(string s, int i, int PivotLevel)
        {
            if (!s.Contains(_Separator) && i != PivotLevel)
                return string.Empty;
            else
            {
                int Index = NthIndexOf(s, _Separator, i);
                if (Index == -1)
                    return s;
                return s.Substring(0, Index);
            }
        }
        /// <summary>
        /// Returns the nth occurance of the SubString from string str
        /// </summary>
        /// <param name="str">source string</param>
        /// <param name="SubString">SubString whose nth occurance to be found</param>
        /// <param name="n">n</param>
        /// <returns>Index of nth occurance of SubString if found else -1</returns>
        private static int NthIndexOf(string str, string SubString, int n)
        {
            int x = -1;
            for (int i = 0; i < n; i++)
            {
                x = str.IndexOf(SubString, x + 1);
                if (x == -1)
                    return x;
            }
            return x;
        }
    }
}
