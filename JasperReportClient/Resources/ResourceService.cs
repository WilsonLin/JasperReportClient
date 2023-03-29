using JasperReports.Module;

namespace JasperReports.Resources
{
    public class ResourceService
    {
        private IResourceService _api;

        internal ResourceService(IResourceService api) { _api = api; }

        /// <summary>
        /// 查詢可用資料庫
        /// </summary>
        /// <returns></returns>
        public ResourceLookupResponse GetDataSource()
        {
            var response = _api.GetResource("jdbcDataSource").Result;
            return response;
        }

        /// <summary>
        /// 查詢可用的Report資料
        /// </summary>
        /// <returns></returns>
        public ResourceLookupResponse GetReportSource()
        {
            var response = _api.GetResource("reportUnit").Result;
            return response;
        }

        /// <summary>
        /// 查詢所有的資料夾
        /// </summary>
        /// <returns></returns>
        public ResourceLookupResponse GetFilesSource()
        {
            var response = _api.GetResource("folder").Result;
            return response;
        }

        /// <summary>
        /// 取得檔案資訊
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ResourcesFilesResponse ResourcesGetFilesInfo(string path)
        {
            var response = _api.ResourcesGetFilesInfo(path).Result;
            return response;
        }

        /// <summary>
        /// 建立Report
        /// </summary>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResourcesFilesResponse CreateResources(string path, CreateResources request)
        {
            var response = _api.CreateResources(path, request).Result;
            return response;
        }

        public ResourcesFilesResponse UpdateResources(string path, CreateResources request)
        {
            var response = _api.UpdateResources(path, true, request).Result;
            return response;
        }
    }
}