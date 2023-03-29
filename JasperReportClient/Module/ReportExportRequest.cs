using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JasperReports.Module
{
    public class ReportExportRequest
    {
        public ExportType outputFormat { get; set; }

        public string attachmentsPrefix = "/jasperserver/rest_v2/reportExecutions/{reportExecutionId}/exports/{exportExecutionId}/attachments/";

        public bool allowInlineScripts = false;
        public string markupType = "embeddable";
        public string baseUrl = "jasperserver";
        public bool ignorePagination = false;
        public bool clearContextCache = true;
    }
}