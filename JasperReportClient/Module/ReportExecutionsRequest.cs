using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JasperReports.Module
{
    public class ReportExecutionsRequest
    {
        public string reportUnitUri { get; set; }

        public bool async = true;
        public bool allowInlineScripts = false;
        public string markupType = "embeddable";
        public bool interactive = true;
        public bool freshData = false;
        public bool saveDataSnapshot = false;
        public string transformerKey { get; set; }
        public long reportContainerWidth = 1920;
        public bool ignorePagination = true;
        public int page = 1;
        public string attachmentsPrefix = "/jasperserver/rest_v2/reportExecutions/{reportExecutionId}/exports/{exportExecutionId}/attachments/";
        public string baseUrl = "jasperserver";
        public ExportType outputFormat { get; set; }
        public ReportExcutionsParamter parameters { get; set; }
    }
}