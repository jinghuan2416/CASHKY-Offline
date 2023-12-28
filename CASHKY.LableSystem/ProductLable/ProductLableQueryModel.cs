using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CASHKY.LableSystem.ProductLable
{
    public class ProductLableQueryModel:ObservableObject
    {
        private string? name;
        private string? yarn;
        private string? store;

        public bool IsSelectedName { get; set; } 
        public bool IsSelectedYarn { get; set; } 
        public bool IsSelectedStore { get; set; }
        public string? Name { get => name; set => this.SetProperty(ref name , value); }
        public string? Yarn { get => yarn; set => this.SetProperty(ref yarn, value); }
        public string? Store { get => store; set => this.SetProperty(ref store, value); }

        internal IQueryable<ProductLableEntity> GetQuery(IQueryable<ProductLableEntity> query)
        {
            if(IsSelectedName && !string.IsNullOrEmpty(Name)) { query = query.Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.Contains(this.Name)); }
            if(IsSelectedYarn && !string.IsNullOrEmpty(Yarn)) { query = query.Where(x => !string.IsNullOrEmpty(x.Yarn) && x.Yarn.Contains(this.Yarn)); }
            if(IsSelectedStore && !string.IsNullOrEmpty(Store)) { query = query.Where(x => !string.IsNullOrEmpty(x.Store) && x.Store.Contains(this.Store)); }
            return query;
        }
    }
}
