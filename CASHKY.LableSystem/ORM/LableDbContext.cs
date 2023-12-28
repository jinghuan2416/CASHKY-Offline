using CASHKY.LableSystem.ProductLable;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASHKY.LableSystem.ORM
{
    public class LableDbContent : DbContext
    {
        static SqliteConnectionStringBuilder builder;
        static LableDbContent()
        {
            builder = new SqliteConnectionStringBuilder
            {
                DataSource = System.IO.Path.Combine( "Database", "Lable.db")
            };
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductLableEntity>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(builder.ConnectionString);
        }
    }
}
