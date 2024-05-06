using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Website.Domain.Abstract
{
    public abstract class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public virtual int Id { get; set; }

        public virtual Guid Guid { get; set; }

        [Column("CreateDate")]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [Column("CreateBy")]
        public int CreatedBy { get; set; }

        [Column("UpdateDate")]
        public DateTime? UpdateDate { get; set; } 

        [Column("UpdateBy")]
        public int? UpdatedBy { get; set; }

    }

}
