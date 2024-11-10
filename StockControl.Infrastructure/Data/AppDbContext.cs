using Microsoft.EntityFrameworkCore;
using StockControl.Domain.Entities;

namespace StockControl.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<StockHistory> StockHistories { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureProductEntity(modelBuilder);
        ConfigureSupplierEntity(modelBuilder);
        ConfigureContactEntity(modelBuilder);
        ConfigureAddressEntity(modelBuilder);
        ConfigureStockHistoryEntity(modelBuilder);
    }

    private static void ConfigureCategoryEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureProductEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(p => p.SkuNumber)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(p => p.Description)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(p => p.CostPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entity.Property(p => p.PricePerUnit)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entity.Property(p => p.Stock)
                .IsRequired();

            entity.Property(p => p.MinStock)
                .IsRequired();

            entity.Property(p => p.MaxStock)
                .IsRequired();

            entity.Property(p => p.ImageUrl)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(p => p.Supplier)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureSupplierEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.BusinessName)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.CNPJ)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnType("varchar(20)");

            entity.HasMany(s => s.Contacts)
                .WithOne(c => c.Supplier)
                .HasForeignKey(c => c.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(s => s.Addresses)
                .WithOne(a => a.Supplier)
                .HasForeignKey(a => a.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureContactEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.ContactType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.ContactName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.Ddd)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.Number)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            entity.HasOne(x => x.Supplier)
                .WithMany(s => s.Contacts)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureAddressEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Street)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.Number)
                .IsRequired();

            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsRequired();

            entity.HasOne(x => x.Supplier)
                .WithMany(s => s.Addresses)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureStockHistoryEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StockHistory>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.ProductSku)
                .IsRequired();

            entity.Property(e => e.Status)
                .IsRequired();

            entity.Property(e => e.ActualStock)
                .IsRequired();

            entity.Property(e => e.OldStock)
                .IsRequired();

            entity.Property(e => e.TransactionType)
                .IsRequired();

            entity.Property(e => e.TimeStamp)
                .IsRequired();

            entity.Property(e => e.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.Units)
                .IsRequired();

            entity.Property(e => e.Category)
                .IsRequired();

            entity.Property(e => e.TotalExpense)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");
        });
    }
}