using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JasperReports.Module
{
    public class ResourceLookup
    {
        public int Version { get; set; }
        public int PermissionMask { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Label { get; set; }
        public string Uri { get; set; }
        public string ResourceType { get; set; }
    }
}