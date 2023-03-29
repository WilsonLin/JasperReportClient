using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JasperReports.Module
{
    public class ResourcesFilesResponse
    {
        public int Version { get; set; }
        public int PermissionMask { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Uri { get; set; }
        public DataSource DataSource { get; set; }
        public Query Query { get; set; }
        public Jrxml Jrxml { get; set; }
        public bool AlwaysPromptControls { get; set; }
        public string ControlsLayout { get; set; }
    }
}