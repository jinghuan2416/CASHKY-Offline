using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASHKY.YarnSystem.YarnCategory
{
    [Table("yarn.categories")]
    public class YarnCategoryEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Name { get; set; }
        public ICollection<YarnWarehousing.YarnWarehousingEntity>? YarnWarehousings { get; set; }
    }
}
