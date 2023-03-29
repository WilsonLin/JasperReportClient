using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JasperReports.Module
{
    public class CreateResources
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public DataSource DataSource { get; set; }
        public Jrxml Jrxml { get; set; }
    }
}