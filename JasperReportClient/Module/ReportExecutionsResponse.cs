using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JasperReports.Module
{
    public class ReportExecutionsResponse
    {
        public string Status { get; set; }
        public int TotalPages { get; set; }
        public string RequestId { get; set; }
        public string ReportURI { get; set; }
        public List<Export> Exports { get; set; }
    }
}