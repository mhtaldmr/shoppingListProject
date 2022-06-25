using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Interfaces.DbContext
{
    public interface IShoppingListDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<ListCategory> ListCategories { get; set; }
        public DbSet<ListCategoryItem> ListCategoryItems { get; set; }
        public DbSet<UnitOfMaterial> UnitOfMaterials { get; set; }
    }
}