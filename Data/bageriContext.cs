using BageriApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Tools;

namespace BageriApi.Data;

public class BageriContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierProduct> SupplierProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Skapat en kolumner som sammansatt primary key...
        modelBuilder.Entity<SupplierProduct>().HasKey(c => new { c.ProductId, c.SupplierId });

        base.OnModelCreating(modelBuilder);
    }
}
