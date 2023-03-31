using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JasperReports.Module
{
    public class OutputResource
    {
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public string Pages { get; set; }
        public bool OutputFinal { get; set; }
        public int OutputTimestamp { get; set; }
    }
}