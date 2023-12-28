using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASHKY.LableSystem.ProductLable
{
    [Table("product.lable")]
    public class ProductLableEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Yarn { get; set; }
        public string? Size1 { get; set; }
        public string? Size2 { get; set; }
        public string? Size3 { get; set; }
        public string? Size4 { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string? Style { get; set; }
        public string? Composition { get; set; }
        public string? Store { get; set; }
    }
}
