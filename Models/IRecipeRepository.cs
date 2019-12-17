using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManagement.Models
{
    public interface IRecipeRepository
    {
        Recipe GetRecipe(int Id);
        IEnumerable<Recipe> GetAllRecipes();
        Recipe Add(Recipe recipe);
        Recipe Update(Recipe recipeChanges);
        Recipe Delete(int Id);
    }
}
