namespace CASHKY.YarnSystem.YarnWarehousing
{
    public class SearchYarnWarehousingModel
    {
        public SearchYarnWarehousingModel()
        {
        }

        public SearchYarnWarehousingModel(long yarnCategoryId)
        {
            YarnCategoryId = yarnCategoryId;
        }

        public bool IsYarnCategoryId { get; set; }
        public bool IsColor { get; set; }
        public bool IsDate { get; set; }
        public long YarnCategoryId { get; set; }
        public string? Color { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);

        internal IQueryable<YarnWarehousingEntity> GetQuery(IQueryable<YarnWarehousingEntity> query)
        {
            if (IsYarnCategoryId ) { query = query.Where(x => x.YarnCategoryId == YarnCategoryId); }
            if (IsColor && !string.IsNullOrEmpty(Color)) { query = query.Where(x => !string.IsNullOrEmpty(x.Color) && x.Color.Contains(this.Color)); }
            if (IsDate ) { query = query.Where(x => x.InventoryDate >= this.StartDate && x.InventoryDate <= this.EndDate); }
            return query;
        }
    }
}
