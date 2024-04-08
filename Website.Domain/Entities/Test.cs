using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Domain.Entities
{
    public class Test
    {
        public int Id { get; set; }

        public int AssetId { get; set; }

        public DateTime ReportDate { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal ResidualValue { get; set; }
    }
}
