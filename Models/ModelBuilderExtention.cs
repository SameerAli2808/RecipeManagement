using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManagement.Models
{
    public static class ModelBuilderExtention
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    Id = 1,
                    Name = "Mary",
                    Source = "mary@abc.com",
                    Ingredient = Ingredient.Rice
                },
                new Recipe
                {
                    Id = 2,
                    Name = "John",
                    Source = "john@abc.com",
                    Ingredient = Ingredient.Oil
                }
                );
        }
    }
}
