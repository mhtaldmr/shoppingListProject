using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Interfaces.DbContext;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Infrastructure.Persistence.DbContext
{
    public class ShoppingListDbContext : IdentityDbContext<User>, IShoppingListDbContext
    {
        public ShoppingListDbContext(DbContextOptions<ShoppingListDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<ListItem> ListItems { get; set; }
        public DbSet<UnitOfMaterial> UnitOfMaterials { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<ListItem>()
            //    .HasKey(li => new { li.ListId, li.ItemId });
        }
    }
}