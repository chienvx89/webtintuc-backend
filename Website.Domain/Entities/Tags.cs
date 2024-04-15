using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Domain.Abstract;

namespace Website.Domain.Entities
{
    public class Tags:Entity
    {
        public string Name { get; set; }
        public string ArticleID { get; set; }
        public string Description { get; set; }
    }
}
