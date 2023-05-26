using Microsoft.EntityFrameworkCore;
using Products.Api.Models;

namespace Products.Api.Data;

public class EcommerceContext : DbContext
{
    public EcommerceContext(DbContextOptions<EcommerceContext> contextOptions) : base(contextOptions)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
}