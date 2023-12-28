using AutoMapper;
using CASHKY.LableSystem.ORM;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using System.Windows;

namespace CASHKY.LableSystem.ProductLable
{
    public partial class ProductLableViewModel : ObservableObject
    {
        public ProductLableEntity SelectedProductLable
        {
            get => selectedProductLable; set
            {
                this.SetProperty(ref selectedProductLable, value);
                if (value != null)
                {
                    this.ProductLableMappingModel = this.mapper.Map<ProductLableMappingModel>(value);
                    this.PrintParameter.ProductInfo = this.mapper.Map<ProductLableMappingModel>(value);
                    this.ProductLableQueryModel = this.mapper.Map<ProductLableQueryModel>(value);
                }
            }
        }
        public int[] PageSizes { get; set; } = [10, 20, 30, 50, 100];
        public int SelectPageSize
        {
            get => selectPageSize;
            set
            {
                if (value != selectPageSize && PageSizes.Contains(value))
                {
                    this.SetProperty(ref selectPageSize, value);
                    Task.Run(this.RefreshAsync);
                }
            }
        }

        public int CurrentPage
        {
            get => currentPage; set
            {
                if (this.SetProperty(ref currentPage, value))
                {
                    Task.Run(this.RefreshAsync);
                }
            }
        }

        [ObservableProperty]
        private ProductLableEntity[] productLableEntities = Array.Empty<ProductLableEntity>();
        [ObservableProperty]
        private ProductLableMappingModel productLableMappingModel = new();
        [ObservableProperty]
        private PrintParameter printParameter = new();
        [ObservableProperty]
        private ProductLableQueryModel productLableQueryModel = new();
        [ObservableProperty]
        private int count;
        private int currentPage = 1;
        [ObservableProperty]
        private int totalPage = 1;
        private int selectPageSize = 20;
        private ProductLableEntity selectedProductLable = new();
        private readonly IDbContextFactory<LableDbContent> dbContextFactory;
        private readonly IMapper mapper;
        private readonly PrintDialog printDialog = new();
        public ProductLableViewModel(IDbContextFactory<LableDbContent> dbContextFactory, IMapper mapper)
        {
            this.dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            Task.Run(this.RefreshAsync);
        }

        [RelayCommand]
        private async Task ResetSearch()
        {
            this.ProductLableQueryModel = new();
            await this.RefreshAsync();
        }
        [RelayCommand]
        private async Task Search()
        {
            await this.RefreshAsync();
        }
        [RelayCommand]
        private void Print(UIElement? element)
        {
            if (printDialog.ShowDialog() == true) { printDialog.PrintVisual(element, ""); }
        }
        [RelayCommand]
        private async Task Delete()
        {

            if (this.SelectedProductLable is null) { return; }
            if (MessageBox.Show("是否继续删除该信息", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                var db = this.dbContextFactory.CreateDbContext();
                var r = db.Set<ProductLableEntity>().AsNoTracking().FirstOrDefault(x => x.Id == SelectedProductLable.Id);
                if (r != null)
                {
                    db.Set<ProductLableEntity>().Remove(r);
                    await db.SaveChangesAsync();
                    await this.RefreshAsync();
                }
            }
        }
        [RelayCommand]
        private async Task Edit()
        {
            if (this.SelectedProductLable is null) { return; }
            var db = this.dbContextFactory.CreateDbContext();
            var r = db.Set<ProductLableEntity>().AsNoTracking().FirstOrDefault(x => x.Id == SelectedProductLable.Id);
            if (r != null)
            {
                this.mapper.Map(this.ProductLableMappingModel, r);
                db.Set<ProductLableEntity>().Update(r);
                await db.SaveChangesAsync();
                await this.RefreshAsync();
            }
        }
        [RelayCommand]
        private async Task Add()
        {
            var db = this.dbContextFactory.CreateDbContext();
            var r = this.mapper.Map<ProductLableEntity>(this.ProductLableMappingModel);
            r.Id = 0;
            db.Set<ProductLableEntity>().Add(r);
            await db.SaveChangesAsync();
            this.CurrentPage = 1;
        }

        private async Task RefreshAsync()
        {
            var size = SelectPageSize;
            var page = CurrentPage;
            var db = this.dbContextFactory.CreateDbContext();
            var query = db.Set<ProductLableEntity>().AsNoTracking();
            if (this.ProductLableQueryModel != null) { query = ProductLableQueryModel.GetQuery(query); }
            var count = query.Count();
            var tp = Math.DivRem(count, size, out int y);
            if (y > 0) { tp += 1; }
            if (page < 1) { page = 1; }
            else if (page > tp) { page = tp; }
            this.ProductLableEntities = await query.OrderByDescending(x => x.Id).Skip((page - 1) * size).Take(size).ToArrayAsync();
            if (this.CurrentPage > tp) { this.SetProperty(ref currentPage, tp); }
            this.TotalPage = tp;
            this.Count = count;
        }
        private void LoadConfig()
        {
            try
            {
                var json = System.IO.File.ReadAllText(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Product.Lable.json"));
                this.PrintParameter = System.Text.Json.JsonSerializer.Deserialize<PrintParameter>(json) ?? new PrintParameter();
            }
            catch (Exception)
            {

            }
        }
    }
}
