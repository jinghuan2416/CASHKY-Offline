using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CASHKY.LableSystem.ProductLable
{
    /// <summary>
    /// ProductLableView.xaml 的交互逻辑
    /// </summary>
    public partial class ProductLableView : UserControl
    {
        public ProductLableView(ProductLableViewModel viewModel)
        {
            InitializeComponent();this.DataContext=viewModel;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox)?.SelectAll();
        }

    }
}
