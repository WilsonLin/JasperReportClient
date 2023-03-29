using Refit;
using JasperReports.Module;
using Iana;

namespace JasperReports.ReportExecutions
{
    public interface IReportExecutionsService
    {
        [Headers($"accept:{MediaTypes.Application.Json}")]
        [Post("/rest_v2/reportExecutions")]
        Task<ReportExecutionsResponse> ReportExecutions([Body] ReportExecutionsRequest request);

        [Headers($"accept:{MediaTypes.Application.Json}")]
        [Post("/rest_v2/reportExecutions/{requestId}/exports")]
        Task<ReportExportResponse> Exports(string requestId, [Body] ReportExportRequest request);

        [Get("/rest_v2/reportExecutions/{requestId}/exports/{exportId}/outputResource")]
        Task<HttpResponseMessage> OutputResource(string requestId, string exportId);
    }
}