using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManagement.Models
{
    public class SQLRecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext context;

        public SQLRecipeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Recipe Add(Recipe recipe)
        {
            context.Recipes.Add(recipe);
            context.SaveChanges();
            return recipe;
        }

        public Recipe Delete(int Id)
        {
            Recipe recipe = context.Recipes.Find(Id);
            if(recipe != null)
            {
                context.Recipes.Remove(recipe);
                context.SaveChanges();
            }
            return recipe;
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            return context.Recipes.ToList();
        }

        public Recipe GetRecipe(int Id)
        {
            return context.Recipes.Find(Id);
        }

        public Recipe Update(Recipe recipeChanges)
        {
            var recipe = context.Recipes.Attach(recipeChanges);
            recipe.State = EntityState.Modified;
            context.SaveChanges();
            return recipeChanges;
        }
    }
}
