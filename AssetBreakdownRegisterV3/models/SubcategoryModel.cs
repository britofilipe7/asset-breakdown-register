using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetBreakdownRegisterV2.models
{
    public class SubcategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
    }
}
