using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CASHKY.YarnSystem.ORM
{
    public class YarnDbContext : DbContext
    {
        static SqliteConnectionStringBuilder builder;
        static YarnDbContext()
        {
            builder = new SqliteConnectionStringBuilder
            {
                DataSource = System.IO.Path.Combine("Database", "Yarn.db")
            };
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<YarnCategory.YarnCategoryEntity>();
            modelBuilder.Entity<YarnWarehousing.YarnWarehousingEntity>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(builder.ConnectionString);
        }


    }
}
