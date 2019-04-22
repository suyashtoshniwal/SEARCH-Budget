using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace System.Web.Mvc
{
    public class Style : System.Web.UI.WebControls.Style
    {
        public bool Bold { get { return this.Font.Bold; } set { this.Font.Bold = value; } }
        public bool Italic { get { return this.Font.Italic; } set { this.Font.Italic = value; } }
        public bool Underline { get { return this.Font.Underline; } set { this.Font.Underline = value; } }
        public string FontName { get { return this.Font.Name; } set { this.Font.Name = value; } }
        
        public Style()
        {
            //Font f = new Font("Arial", 9.8f);
        }
    }
}
