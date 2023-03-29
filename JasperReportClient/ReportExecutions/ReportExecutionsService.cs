using JasperReports.Module;

namespace JasperReports.ReportExecutions
{
    public class ReportExecutionsService
    {
        private IReportExecutionsService _api;

        internal ReportExecutionsService(IReportExecutionsService api) { _api = api; }
        public ReportExecutionsResponse ReportExecutions(ReportExecutionsRequest request)
        {
            var response = _api.ReportExecutions(request).Result;
            return response;
        }
        public ReportExportResponse Exports(string requestId, ReportExportRequest request)
        {
            var response = _api.Exports(requestId, request).Result;
            return response;
        }
        public byte[] Download(string requestId, string exportId)
        {
            var response = _api.OutputResource(requestId, exportId).Result;
            return response.Content.ReadAsByteArrayAsync().Result;
        }
    }
}