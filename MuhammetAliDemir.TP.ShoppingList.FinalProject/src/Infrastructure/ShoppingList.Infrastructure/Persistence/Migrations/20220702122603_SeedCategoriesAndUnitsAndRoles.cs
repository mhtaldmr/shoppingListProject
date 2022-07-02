using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingList.Infrastructure.Persistence.Migrations
{
    public partial class SeedCategoriesAndUnitsAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into \"UnitOfMaterials\"(\"Id\", \"UoMCode\", \"Description\", \"CreatedAt\") values (1,'KG','Kilogram','01.01.2022')");
            migrationBuilder.Sql("insert into \"UnitOfMaterials\"(\"Id\", \"UoMCode\", \"Description\", \"CreatedAt\") values (2,'GR','Gram','01.01.2022')");
            migrationBuilder.Sql("insert into \"UnitOfMaterials\"(\"Id\", \"UoMCode\", \"Description\", \"CreatedAt\") values (3,'LT','Liter','01.01.2022')");
            migrationBuilder.Sql("insert into \"UnitOfMaterials\"(\"Id\", \"UoMCode\", \"Description\", \"CreatedAt\") values (4,'BOX','Box','01.01.2022')");
            migrationBuilder.Sql("insert into \"UnitOfMaterials\"(\"Id\", \"UoMCode\", \"Description\", \"CreatedAt\") values (5,'PIECE','Piece','01.01.2022')");
            
            migrationBuilder.Sql("insert into \"Categories\"(\"Id\", \"Name\", \"Description\", \"CreatedAt\") values (1,'Market','Weekly Shopping..','01.01.2022')");
            migrationBuilder.Sql("insert into \"Categories\"(\"Id\", \"Name\", \"Description\", \"CreatedAt\") values (2,'School','Notebooks and papers..','01.01.2022')");
            migrationBuilder.Sql("insert into \"Categories\"(\"Id\", \"Name\", \"Description\", \"CreatedAt\") values (3,'Technology','Phones and pcs..','01.01.2022')");
            migrationBuilder.Sql("insert into \"Categories\"(\"Id\", \"Name\", \"Description\", \"CreatedAt\") values (4,'Car','Wheelcovers etc..','01.01.2022')");
            migrationBuilder.Sql("insert into \"Categories\"(\"Id\", \"Name\", \"Description\", \"CreatedAt\") values (5,'Grocery','Food and vegetables','01.01.2022')");
 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from \"UnitOfMaterials\"");
            migrationBuilder.Sql("delete from \"Categories\"");
        }
    }
}
