using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JasperReports.Module
{
    public class JrxmlFile
    {
        public string Type { get; set; }
        public string Label { get; set; }

        /// <summary>
        /// base64
        /// </summary>
        /// <value></value>
        public string Content { get; set; }

        public JrxmlFile(string type, string label, string base64Content)
        {
            this.Type = type;
            this.Label = label;
            this.Content = base64Content;
        }
    }
}