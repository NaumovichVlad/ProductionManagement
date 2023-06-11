using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Config
{
    public class ProductionManagementDbContext : DbContext
    {
        public ProductionManagementDbContext(DbContextOptions<ProductionManagementDbContext> options)
            : base(options)
        { }

        public ProductionManagementDbContext() => Database.EnsureCreated();

        public DbSet<Counteragent> Counteragents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<FinishedProduct> FinishedProducts { get; set; }
        public DbSet<FinishedProductSale> FinishedProductsSales { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<MaterialReserve> MaterialReserves { get; set; }
        public DbSet<MaterialsForFinishedProducts> MaterialsForFinishedProducts { get; set; }
        public DbSet<MaterialsForProducts> MaterialsForProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ProductsReserve> ProductsReserves { get; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<StoragePlace> StoragePlaces { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MaterialPurchase> MaterialsPurchases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=admin;database=production_management",
                new MySqlServerVersion(new Version(8, 0, 25)));
        }
    }
}
