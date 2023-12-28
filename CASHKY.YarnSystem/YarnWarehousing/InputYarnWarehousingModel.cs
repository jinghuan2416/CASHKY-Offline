using CASHKY.YarnSystem.YarnCategory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CASHKY.YarnSystem.YarnWarehousing
{
    public class InputYarnWarehousingModel
    {
        public InputYarnWarehousingModel()
        {
        }

        public InputYarnWarehousingModel(long yarnCategoryId)
        {
            YarnCategoryId = yarnCategoryId;
        }

        public long Id { get; set; } = 0;

        public long YarnCategoryId { get; set; }

        [Required(ErrorMessage = "色号不能为空")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "缸号不能为空")]
        public string? Batch { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "重量不能小于0")]
        public double Weight { get; set; } = 0;
        public DateTime InventoryDate { get; set; } = DateTime.Now;
        public string? Remarks { get; set; }

        public bool IsValid(out string mess)
        {
            mess = string.Empty;
            foreach (var p in this.GetType().GetProperties())
            {
                var att = p.GetCustomAttribute<ValidationAttribute>();
                if (att is not null)
                {
                    if (!att.IsValid(p.GetValue(this)))
                    {
                        mess = att.ErrorMessage ?? $"{p.Name}参数错误"; return true;
                    }
                }
            }
            return false;
        }
    }
}
