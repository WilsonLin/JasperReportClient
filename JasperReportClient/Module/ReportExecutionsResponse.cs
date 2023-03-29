using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JasperReports.Module
{
    public class ReportExecutionsResponse
    {
        public string status { get; set; }
        public string requestId { get; set; }
        public string reportURI { get; set; }
    }
}