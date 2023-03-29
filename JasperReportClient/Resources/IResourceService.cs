using Refit;
using JasperReports.Module;
using Iana;

namespace JasperReports.Resources
{
    public interface IResourceService
    {
        /// <summary>
        /// 查詢資源
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [Headers($"accept:{MediaTypes.Application.Json}")]
        [Get("/rest_v2/resources")]
        Task<ResourceLookupResponse> GetResource([Query] string type);


        /// <summary>
        /// 取得檔案詳細資訊
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Headers($"accept:{MediaTypes.Application.Json}")]
        [Get("/rest_v2/resources/{path}")]
        [QueryUriFormat(UriFormat.Unescaped)] // 避免path有`/`時被encode
        Task<ResourcesFilesResponse> ResourcesGetFilesInfo(string path);

        /// <summary>
        /// 建立JasperReport
        /// </summary>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Headers($"accept:{MediaTypes.Application.Json}", $"Content-Type:{MediaTypes.Application.ReportUnitJson}")]
        [Post("/rest_v2/resources/{path}")]
        [QueryUriFormat(UriFormat.Unescaped)]
        Task<ResourcesFilesResponse> CreateResources(string path, [Body] CreateResources request);

        /// <summary>
        /// 修改Report資源
        /// </summary>
        /// <param name="path"></param>
        /// <param name="overwrite"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Headers($"accept:{MediaTypes.Application.Json}", $"Content-Type:{MediaTypes.Application.ReportUnitJson}")]
        [Put("/rest_v2/resources/{path}")]
        [QueryUriFormat(UriFormat.Unescaped)]
        Task<ResourcesFilesResponse> UpdateResources(string path, [Query] bool overwrite, [Body] CreateResources request);

        /// <summary>
        /// 刪除資源
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Delete("/rest_v2/resources/{path}")]
        [QueryUriFormat(UriFormat.Unescaped)]
        Task<ResourcesFilesResponse> DeleteResources(string path);
    }
}