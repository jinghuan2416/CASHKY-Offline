using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CASHKY.YarnSystem.YarnCategory;

namespace CASHKY.YarnSystem.YarnWarehousing
{
    [Table("yarn.warehousing")]
    public class YarnWarehousingEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } = 0;
        public long YarnCategoryId { get; set; } = 0;
        public YarnCategoryEntity? YarnCategory { get; set; }
        public string? Color { get; set; }
        public string? Batch { get; set; }
        public double Weight { get; set; } = 0;
        public DateTime InventoryDate { get; set; } = DateTime.Now;
        public string? Remarks { get; set; }
    }
}
