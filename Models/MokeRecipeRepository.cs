using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManagement.Models
{
    public class MokeRecipeRepository : IRecipeRepository
    {
        private List<Recipe> _recipeList;

        public MokeRecipeRepository()
        {
            _recipeList = new List<Recipe>()
            {
                new Recipe(){ Id = 1, Name="Mary", Ingredient=Ingredient.Rice, Source="mary@abc.com" },
                new Recipe(){ Id = 2, Name="John", Ingredient=Ingredient.Oil, Source="john@abc.com" },
                new Recipe(){ Id = 3, Name="Sam", Ingredient=Ingredient.Salt, Source="sam@abc.com" }
            };
        }

        public Recipe Add(Recipe recipe)
        {
            recipe.Id = _recipeList.Max(e => e.Id) + 1;
            _recipeList.Add(recipe);
            return recipe;
        }

        public Recipe Delete(int Id)
        {
            Recipe recipe = _recipeList.FirstOrDefault(e => e.Id == Id);
            if (recipe != null)
            {
                _recipeList.Remove(recipe);
            }
            return recipe;
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            return _recipeList;
        }

        public Recipe GetRecipe(int Id)
        {
            return _recipeList.FirstOrDefault(e => e.Id == Id);
        }

        public Recipe Update(Recipe recipeChanges)
        {
            Recipe recipe = _recipeList.FirstOrDefault(e => e.Id == recipeChanges.Id);
            if (recipe != null)
            {
                recipe.Name = recipeChanges.Name;
                recipe.Source = recipeChanges.Source;
                recipe.Ingredient = recipeChanges.Ingredient;
            }
            return recipe;
        }
    }
}
