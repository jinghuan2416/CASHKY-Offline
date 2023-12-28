using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CASHKY
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private object? content;
        private IServiceProvider serviceProvider;
        public List<NavItem> NavItems { get; set; } = new();


        public MainWindowViewModel(IServiceProvider serviceProvider)
        {
            NavItems.Add(new NavItem("文件"));
            NavItems.Add(new NavItem("标签", typeof(CASHKY.LableSystem.ProductLable.ProductLableView)));
            NavItems.Add(new NavItem("纱线"));
            NavItems.Last().NavItems.Add(new NavItem("类型", typeof(CASHKY.YarnSystem.YarnCategory.YarnCategoryView)));
            NavItems.Last().NavItems.Add(new NavItem("入库", typeof(CASHKY.YarnSystem.YarnWarehousing.YarnWarehousingView)));
            this.serviceProvider = serviceProvider;
        }

        [RelayCommand]
        private void Navigation(Type? type)
        {
            if (type is null) { return; }
            this.Content = this.serviceProvider.GetService(type);
        }
    }
}