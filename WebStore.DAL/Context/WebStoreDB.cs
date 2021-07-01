using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entitys;

namespace WebStore.DAL.Context
{
    namespace WebStore.DAL.Context
    {
        public class WebStoreDB : DbContext
        {
            public DbSet<Product> Products { get; set; }

            public DbSet<Section> Sections { get; set; }

            public DbSet<Brand> Brands { get; set; }

            public WebStoreDB(DbContextOptions<WebStoreDB> options) : base(options) { }
        }
    }
}
