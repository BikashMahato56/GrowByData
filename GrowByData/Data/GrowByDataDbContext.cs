using GrowByData.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrowByData.Data
{
    public class GrowByDataDbContext: DbContext
    {
        public GrowByDataDbContext(DbContextOptions<GrowByDataDbContext> options):base(options)
        {

        }
        //public DbSet<Product> Product { get; set; } 
        //public DbSet<ProductList> ProductList { get; set; } 
    }
}
