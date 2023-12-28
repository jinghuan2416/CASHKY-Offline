using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CASHKY.LableSystem.ProductLable
{
  public partial  class PrintParameter:ObservableObject
    {
        [ObservableProperty]
        private ProductLableMappingModel productInfo = new();
        [ObservableProperty]
        private Thickness margin = new Thickness(5, 10 / 25.4 * 96, 5, 5);
        [ObservableProperty]
        private double height = 70 / 25.4 * 96;
        [ObservableProperty]
        private double width = 30 / 25.4 * 96;
        [ObservableProperty]
        private double fontSize = 11;
    }
}
