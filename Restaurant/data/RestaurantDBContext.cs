using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Net.Sockets;
using System;
using System.Reflection.Emit;
using System.Windows.Controls;
using System.Collections.Generic;
using Restaurant.app.model;

namespace Restaurant;
public class RestaurantDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> Roles { get; set; }
    public DbSet<AccessRight> AccessRights { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<UnitOfMeasure> UnitsOfMeasure { get; set; }
    public DbSet<DishGroup> DishGroups { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Menu> Menu { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Supply> Supplies { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet <DishesProducts> DishesProducts { get; set; }
    public DbSet <OrdersDishes> OrdersDishes { get; set; }
    public DbSet<SuppliesProducts> SuppliesProducts { get; set; }



    // public DbSet<Department> Departments { get; set; }
    // public DbSet<DishProduct> DishesProducts { get; set; }
    // public DbSet<SupplyProduct> SuppliesProducts { get; set; }
    // public DbSet<RequestProduct> RequestsProducts { get; set; } ??????

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host = localhost; Port = 5432; Database = restaurantdb; Username = admin; Password = admin");
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().Ignore(o => o.OrderedDishes);
        modelBuilder.Entity<DishesProducts>().HasKey(dp => new { dp.DishID, dp.ProductID });
        modelBuilder.Entity<SuppliesProducts>().HasKey(dp => new { dp.SupplyID, dp.ProductID });
        modelBuilder.Entity<OrdersDishes>().HasKey(dp => new { dp.OrderID, dp.DishID});
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>().HaveConversion<UtcValueConverter>();
        configurationBuilder.Properties<DateTime?>().HaveConversion<UtcValueConverter>();
        base.ConfigureConventions(configurationBuilder);
    }

    private class UtcValueConverter : ValueConverter<DateTime, DateTime>
    {
        public UtcValueConverter()
            : base(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
        {
        }
    }
}
