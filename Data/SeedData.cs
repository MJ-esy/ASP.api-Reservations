using ASP_Reservations.Models;
using ASP_Reservations.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace ASP_Reservations.Data
{
    public static class SeedData
    {
        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Name = "Test One", Email = "One@email.com", Phone = "123-456-7890" },
                new User { UserId = 2, Name = "Test Two", Email = "Two@email.com", Phone = "234-567-8901" },
                new User { UserId = 3, Name = "Test Three", Email = "Three@email.com", Phone = "345-678-9012" }
            );
        }
        public static void SeedTables(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>().HasData(
                new Table { TableId = 1, TableNum = 101, Capacity = 2 },
                new Table { TableId = 2, TableNum = 102, Capacity = 2 },
                new Table { TableId = 3, TableNum = 201, Capacity = 4 },
                new Table { TableId = 4, TableNum = 202, Capacity = 4 },
                new Table { TableId = 5, TableNum = 203, Capacity = 4 },
                new Table { TableId = 6, TableNum = 204, Capacity = 4 },
                new Table { TableId = 7, TableNum = 301, Capacity = 8 }
            );
        }

        public static void SeedDishes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>().HasData(
                new Dish
                {
                    DishId = 1,
                    DishName = "Skagen Sandwich",
                    Price = 175, 
                    Description = "sourdough bread, pickled cucumber trout roe(E,G,L)",
                    IsPopular = true,
                    Category = DishCategories.Starters,
                    Allergen = Allergy.Gluten | Allergy.Eggs | Allergy.Lactose
                },
                new Dish
                {
                    DishId = 2,
                    DishName = "Skagen Sandwich",
                    Price = 245,
                    Description = "sourdough bread, pickled cucumber trout roe (E,G,L)",
                    IsPopular = false,
                    Category = DishCategories.Starters,
                    Allergen = Allergy.Gluten | Allergy.Eggs | Allergy.Lactose
                },
                new Dish
                {
                    DishId = 3,
                    DishName = "Chantarelle toast",
                    Price = 160,
                    Description = "parmesan grilled sour dough (G,L)",
                    IsPopular = true,
                    Category = DishCategories.Starters,
                    Allergen = Allergy.Gluten | Allergy.Lactose
                },
                new Dish
                {
                    DishId = 4,
                    DishName = "Smoked Tiger shrimps",
                    Price = 175,
                    Description = "pickled red onions, new potatoes (L)",
                    IsPopular = false,
                    Category = DishCategories.Starters,
                    Allergen = Allergy.Lactose
                },
                new Dish
                {
                    DishId = 5,
                    DishName = "Steak Tartare",
                    Price = 185,
                    Description = "angel fries, parmesan, grilled heart salad",
                    IsPopular = false,
                    Category = DishCategories.Starters
                },
                new Dish
                {
                    DishId = 6,
                    DishName = "Nicoise",
                    Price = 275,
                    Description = "tuna, egg, capers",
                    IsPopular = false,
                    Category = DishCategories.Salad,
                    Allergen = Allergy.Eggs
                },
                new Dish
                {
                    DishId = 7,
                    DishName = "Moules frites",
                    Price = 285,
                    Description = "aioli (E,L)",
                    IsPopular = false,
                    Category = DishCategories.Mains,
                    Allergen = Allergy.Lactose | Allergy.Eggs
                },
                  new Dish
                  {
                      DishId = 8,
                      DishName = "Baked Trout",
                      Price = 295,
                      Description = "Sandefjord sauce, trout roe, Chantarelles, boiled potatoes (L)",
                      IsPopular = true,
                      Category = DishCategories.Mains,
                      Allergen = Allergy.Lactose
                  },
                  new Dish
                  {
                      DishId = 9,
                      DishName = "Ossobuco",
                      Price = 295,
                      Description = "veal, crushed potatoes, red wine sauce, creamy gremolata (E,G,L)",
                      IsPopular = false,
                      Category = DishCategories.Mains,
                      Allergen = Allergy.Lactose | Allergy.Gluten | Allergy.Eggs
                  },
                  new Dish
                  {
                      DishId = 10,
                      DishName = "Pepper Steak",
                      Price = 395,
                      Description = "green peppar sauce, French fries, parmesan (L)",
                      IsPopular = true,
                      Category = DishCategories.Mains,
                      Allergen = Allergy.Lactose
                  },
                  new Dish
                  {
                      DishId = 11,
                      DishName = "Apple pie",
                      Price = 130,
                      Description = "vanilla ice cream (E,L)",
                      IsPopular = true,
                      Category = DishCategories.Dessert,
                      Allergen = Allergy.Lactose | Allergy.Gluten | Allergy.Eggs
                  },
                  new Dish
                  {
                      DishId = 12,
                      DishName = "Creme brulee",
                      Price = 110,
                      Description = "(E,L)",
                      IsPopular = false,
                      Category = DishCategories.Dessert,
                      Allergen = Allergy.Lactose | Allergy.Eggs
                  },
                  new Dish
                  {
                      DishId = 13,
                      DishName = "Chocolate truffle",
                      Price = 45,
                      Description = "(E,G,L,N)",
                      IsPopular = false,
                      Category = DishCategories.Dessert,
                      Allergen = Allergy.Lactose | Allergy.Eggs | Allergy.Gluten | Allergy.Nuts
                  }

            );
        }
    }
}
