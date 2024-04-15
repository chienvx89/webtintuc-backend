using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Domain.Abstract;

namespace Website.Domain.Entities
{
    public class Articles : Entity
    {
        public string Title { get; set; } 
        public string Content { get; set; } 
        public DateTime PublishDate { get; set; }
        public int AuthorID { get; set; }
        public int CategoryID { get; set; }
    }


}
