using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Domain.Abstract;

namespace Website.Domain.Entities
{
    public class Videos: Entity
    {
        public string ArticleID { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
    }
}
