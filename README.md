# JasperReportClient

c#操作JasperReport的解決方案

目前僅實現上傳report與下載pdf,xls,cvs,docx

----------

使用方式
```
JasperReportClient client = JasperReportClient.Create("http://localhost:8080/jasperserver", "jasperadmin", "bitnami");
```

----------

上傳檔案
```
string createPath ="/Your/Path"
CreateResources dd = new CreateResources();
DataSource dataSource = new DataSource();
DataSourceReference refs = new DataSourceReference("/your/datasource/path");
dataSource.DataSourceReference = refs;
dd.DataSource = dataSource;
dd.Label = "projectName";
Jrxml jx = new Jrxml();
JrxmlFile jf = new JrxmlFile("jrxml", "fileName", filesBase64Content);
jx.JrxmlFile = jf;
dd.Jrxml = jx;
client.Resource.CreateResources(createPath, dd);
```

----------

下載檔案
```
ExportType exportType = (ExportType)Enum.Parse(typeof(ExportType), "pdf");
ReportExecutionsRequest req = new ReportExecutionsRequest();
req.reportUnitUri = "/your/report/file/path/";
req.outputFormat = exportType;
ReportExecutionsResponse executions = client.ReportExecutions.ReportExecutions(req);
byte[] pdfBytes = client.ReportExecutions.Download(executions.RequestId, executions.Exports[0].Id);
```

----------

# License #

----------

[MIT](https://github.com/WilsonLin/JasperReportClient/blob/main/LICENSE "MIT") License.
