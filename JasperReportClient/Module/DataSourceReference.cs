using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JasperReports.Module
{
    public class DataSourceReference
    {
        public string Uri { get; set; }
        public int Version { get; set; }
        public DataSourceReference(string url)
        {
            this.Uri = url;
        }

    }
}