using AutoMapper;
using CASHKY.YarnSystem.ORM;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CASHKY.YarnSystem.YarnWarehousing
{
    public partial class YarnWarehousingViewModel : ObservableObject
    {
        [ObservableProperty]
        private YarnWarehousingEntity[] yarnWarehousings = [];
        private YarnWarehousingEntity? selectedYarnWarehousing;
        [ObservableProperty]
        private InputYarnWarehousingModel? inputYarnWarehousing = new();
        [ObservableProperty]
        private SearchYarnWarehousingModel? searchYarnWarehousing = new();
        [ObservableProperty]
        private KeyValuePair<long, string?>[] yarnCategories = [];
        [ObservableProperty]
        private int count;
        private int currentPage = 1;
        [ObservableProperty]
        private int totalPage = 1;
        private int selectPageSize = 20;
        private readonly IDbContextFactory<YarnDbContext> dbContextFactory;
        private readonly IMapper mapper;
        public int[] PageSizes { get; set; } = [10, 20, 30, 50, 100];
        public IDbContextFactory<YarnDbContext> DbContextFactory => dbContextFactory;
        public YarnWarehousingEntity? SelectedYarnWarehousing
        {
            get => selectedYarnWarehousing; set
            {
                this.SetProperty(ref selectedYarnWarehousing, value);
                this.InputYarnWarehousing = value is not null ? mapper.Map<InputYarnWarehousingModel>(value) : new(this.YarnCategories.FirstOrDefault().Key);
                if (value is not null)
                {
                    this.SearchYarnWarehousing = mapper.Map<SearchYarnWarehousingModel>(value);
                }
            }
        }
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

        public YarnWarehousingViewModel(IDbContextFactory<YarnDbContext> dbContextFactory, IMapper mapper)
        {
            this.dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            Task.Run(this.InitializeDataAsync);
        }

        [RelayCommand]
        private async Task AddAsync()
        {
            if (this.InputYarnWarehousing is null) { this.InputYarnWarehousing = new(this.YarnCategories.FirstOrDefault().Key); return; }
            if (this.InputYarnWarehousing.IsValid(out string res)) { MessageBox.Error(res); return; }
            var entity = mapper.Map<YarnWarehousingEntity>(this.InputYarnWarehousing);
            entity.Id = 0;
            using var db = await this.DbContextFactory.CreateDbContextAsync();
            await db.AddAsync(entity);
            await db.SaveChangesAsync();
            await this.RefreshAsync();
        }
        [RelayCommand]
        private async Task ModifyAsync()
        {
            if (this.InputYarnWarehousing is null) { return; }
            if (this.InputYarnWarehousing.IsValid(out string res)) { MessageBox.Error(res); return; }
            using var db = await this.DbContextFactory.CreateDbContextAsync();
            var entity = await db.FindAsync<YarnWarehousingEntity>(this.InputYarnWarehousing.Id);
            if (entity is null) { return; }
            mapper.Map(this.InputYarnWarehousing, entity);
            db.Update(entity);
            await db.SaveChangesAsync();
            await this.RefreshAsync();
        }
        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (this.InputYarnWarehousing is null) { return; }
            using var db = await this.DbContextFactory.CreateDbContextAsync();
            var entity = await db.FindAsync<YarnWarehousingEntity>(this.InputYarnWarehousing.Id);
            if (entity is null) { return; }
            db.Remove(entity);
            await db.SaveChangesAsync();
            await this.RefreshAsync();
        }
        [RelayCommand]
        private async Task InitializeDataAsync()
        {
            await this.RefreshAsync();
            await this.RefreshSelectedAsync();
        }
        [RelayCommand]
        private async Task SearchAsync()
        {
            await this.RefreshAsync();
        }
        [RelayCommand]
        private async Task ResetAsync()
        {
            this.SearchYarnWarehousing = new(this.YarnCategories.FirstOrDefault().Key);
            await this.RefreshAsync();
        }

        private async Task RefreshAsync()
        {
            var size = SelectPageSize;
            var page = CurrentPage;
            using var db = await this.DbContextFactory.CreateDbContextAsync();
            var query = db.Set<YarnWarehousingEntity>().AsNoTracking();
            if (this.SearchYarnWarehousing is not null) { query = this.SearchYarnWarehousing.GetQuery(query); }
            var count = query.Count();
            var tp = Math.DivRem(count, size, out int y);
            if (y > 0) { tp += 1; }
            if (page < 1) { page = 1; }
            else if (page > tp) { page = tp; }
            this.YarnWarehousings = await query.Include(x => x.YarnCategory).OrderByDescending(x => x.Id).Skip((page - 1) * size).Take(size).ToArrayAsync();
            if (this.CurrentPage > tp) { this.SetProperty(ref currentPage, tp); }
            this.TotalPage = tp;
            this.Count = count;
            this.InputYarnWarehousing = new(this.YarnCategories.FirstOrDefault().Key);
        }
        private async Task RefreshSelectedAsync()
        {
            using var db = await this.DbContextFactory.CreateDbContextAsync();
            this.YarnCategories = await db.Set<YarnCategory.YarnCategoryEntity>().OrderByDescending(x => x.Id).Select(x => new KeyValuePair<long, string?>(x.Id, x.Name)).ToArrayAsync();
            this.InputYarnWarehousing = new(this.YarnCategories.FirstOrDefault().Key);
            this.SearchYarnWarehousing = new(this.YarnCategories.FirstOrDefault().Key);
        }
    }


}
