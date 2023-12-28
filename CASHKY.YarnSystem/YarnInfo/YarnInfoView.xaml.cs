using AutoMapper;
using CASHKY.YarnSystem.ORM;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
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

namespace CASHKY.YarnSystem.YarnInfo
{
    /// <summary>
    /// YarnInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class YarnInfoView : UserControl
    {
        public YarnInfoView()
        {
            InitializeComponent();
        }
    }

    public partial class YarnInfoViewModel : ObservableObject
    {
        [ObservableProperty]
        private YarnInfoEntity[] yarnInfoEntities = [];
        [ObservableProperty]
        private YarnInfoEntity? selectedYarnInfoEntity;

        private readonly IDbContextFactory<ORM.YarnDbContext> dbContextFactory;
        private readonly IMapper mapper;

        public YarnInfoViewModel(IDbContextFactory<YarnDbContext> dbContextFactory, IMapper mapper)
        {
            this.dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [RelayCommand]
        private async Task AddAsync()
        {
            var entity = mapper.Map<YarnInfoEntity>(this.SelectedYarnInfoEntity);
            entity.Id = 0;
            using var db = await this.dbContextFactory.CreateDbContextAsync();
            await db.AddAsync(entity);
            await db.SaveChangesAsync();
            await this.RefreshAsync();
        }

        private async Task ModifyAsync()
        {
            if (this.SelectedYarnInfoEntity is null) { return; }
            using var db = await this.dbContextFactory.CreateDbContextAsync();
            var entity = await db.FindAsync<YarnInfoEntity>(this.SelectedYarnInfoEntity.Id);
            if (entity is null) { return; }
            mapper.Map(this.SelectedYarnInfoEntity, entity);
            await db.AddAsync(entity);
            await db.SaveChangesAsync();
            await this.RefreshAsync();
        }


        private async Task RefreshAsync()
        {
            using var db = await this.dbContextFactory.CreateDbContextAsync();
            this.YarnInfoEntities = await db.Set<YarnInfoEntity>().AsNoTracking().ToArrayAsync();
        }
    }
}
