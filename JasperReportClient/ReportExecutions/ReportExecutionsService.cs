using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Extensions;
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
            byte[] result = null;
            var response = _api.OutputResource(requestId, exportId).Result;
            if (response.StatusCode == HttpStatusCode.OK) // FIXME 下載有機率會失敗!!
            {
                // FIXME 每次下載的檔案大小都不同很奇怪!
                result = response.Content.ReadAsByteArrayAsync().Result;
            }
            return result;
        }
    }
}