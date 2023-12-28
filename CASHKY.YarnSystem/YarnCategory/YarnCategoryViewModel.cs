using AutoMapper;
using CASHKY.YarnSystem.ORM;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Microsoft.EntityFrameworkCore;

namespace CASHKY.YarnSystem.YarnCategory
{
    public partial class YarnCategoryViewModel : ObservableObject
    {
        [ObservableProperty]
        private YarnCategoryEntity[] yarnCategories = [];

        private YarnCategoryEntity? selectedYarnCategory;

        [ObservableProperty]
        private YarnCategoryEntity? inputYarnCategory = new();

        private readonly IDbContextFactory<YarnDbContext> dbContextFactory;
        private readonly IMapper mapper;

        public IDbContextFactory<YarnDbContext> DbContextFactory => dbContextFactory;
        public YarnCategoryEntity? SelectedYarnCategory
        {
            get => selectedYarnCategory; set
            {
                this.SetProperty(ref selectedYarnCategory, value);
                this.InputYarnCategory = value is not null ? mapper.Map<YarnCategoryEntity>(value) : new();
            }
        }

        public YarnCategoryViewModel(IDbContextFactory<YarnDbContext> dbContextFactory, IMapper mapper)
        {
            this.dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            Task.Run(this.RefreshAsync);
        }

        [RelayCommand]
        private async Task AddAsync()
        {
            if (this.InputYarnCategory == null) { InputYarnCategory = new(); return; }
            this.InputYarnCategory.Id = 0;
            using var db = await this.DbContextFactory.CreateDbContextAsync();
            await db.AddAsync(this.InputYarnCategory);
            await db.SaveChangesAsync();
            await this.RefreshAsync();
            this.InputYarnCategory = new YarnCategoryEntity();
        }
        [RelayCommand]
        private async Task ModifyAsync()
        {
            if (this.InputYarnCategory is null) { return; }
            using var db = await this.DbContextFactory.CreateDbContextAsync();
            var entity = await db.FindAsync<YarnCategoryEntity>(this.InputYarnCategory.Id);
            if (entity is null) { return; }
            if (MessageBox.Ask($"是否修改【{entity.Name}】纱线？") == System.Windows.MessageBoxResult.OK)
            {
                mapper.Map(this.InputYarnCategory, entity);
                db.Update(entity);
                await db.SaveChangesAsync();
                await this.RefreshAsync();
            }
        }
        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (this.InputYarnCategory is null) { return; }
            using var db = await this.DbContextFactory.CreateDbContextAsync();

            var count= await db.Set<YarnWarehousing.YarnWarehousingEntity>().CountAsync(x => x.YarnCategoryId == this.InputYarnCategory.Id);
            if (count > 0) { MessageBox.Error("当前纱线存在入库记录，无法删除");return; }

            var entity = await db.FindAsync<YarnCategoryEntity>(this.InputYarnCategory.Id);
            if (entity is null) { return; }
            if (MessageBox.Ask($"是否删除【{entity.Name}】纱线？") == System.Windows.MessageBoxResult.OK)
            {
                db.Remove(entity);
                await db.SaveChangesAsync();
                await this.RefreshAsync();
            }
        }

        private async Task RefreshAsync()
        {
            using var db = await this.DbContextFactory.CreateDbContextAsync();
            this.YarnCategories = await db.Set<YarnCategoryEntity>().AsNoTracking().OrderByDescending(x => x.Id).ToArrayAsync();
            this.SelectedYarnCategory = null;
        }
    }
}
