using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetBreakdownRegisterV2.models
{
    public class BreakdownModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Request_date { get; set; }
        public string Status { get; set; }
        public string Register_date { get; set; }
        public int Severity { get; set; }
        public string Observations { get; set; }
        public string State { get; set; }
        public string Color { get; set; }
        public double Cost { get; set; }


    }
}
