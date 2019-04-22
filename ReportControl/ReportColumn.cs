using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace System.Web.Mvc
{
    public class ReportColumn
    {
        public string HeaderText { get; set; }
        public string TemplateItem { get; set; }
        public Style style { get; set; }

        public ReportColumn(string headerText, string templateItem)
        {
            Initialize(headerText, templateItem, (Style)null);
        }
        public ReportColumn(string headerText, string templateItem, Style style)
        {
            Initialize(headerText, templateItem, style);
        }

        public ReportColumn(string headerText, string templateItem, string cssClass)
        {
            Initialize(headerText, templateItem, new Style { CssClass = cssClass });
        }

        //Aggregate Functions.//
        public static ReportColumn Max(params string[] columnNames)
        {
            return new ReportColumn("Not Implemented", "Not Implemented");
        }

        public static ReportColumn Diff(params string[] columnNames)
        {
            return new ReportColumn("Not Implemented", "Not Implemented");
        }

        //Column Types.//
        public static ReportColumn Url(string text, string actionName, string controllerName)
        {
            return new ReportColumn("Not Implemented", "Not Implemented");
        }

        private void Initialize(string headerText, string templateItem, Style style)
        {
            this.HeaderText = headerText;
            this.TemplateItem = templateItem;
            this.style = style;
        }
    }
}
