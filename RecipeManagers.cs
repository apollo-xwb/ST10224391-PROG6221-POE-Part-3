using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ST10224391_PROG6221_POE_Part_3
{
    public class RecipeManager
    {
        public ObservableCollection<Recipe> Recipes { get; private set; }

        public RecipeManager()
        {
            Recipes = new ObservableCollection<Recipe>();
        }

        public void AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
            SortRecipesByName();
        }

        public void SortRecipesByName()
        {
            var sortedRecipes = new ObservableCollection<Recipe>(Recipes.OrderBy(r => r.Name));
            Recipes.Clear();
            foreach (var recipe in sortedRecipes)
            {
                Recipes.Add(recipe);
            }
        }

        public Recipe GetRecipe(string name)
        {
            return Recipes.FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Recipe> FilterRecipes(string ingredientName, string foodGroup, int? maxCalories)
        {
            var filteredRecipes = Recipes.Where(r =>
                (string.IsNullOrEmpty(ingredientName) || r.Ingredients.Any(i => i.Name.IndexOf(ingredientName, StringComparison.OrdinalIgnoreCase) >= 0)) &&
                (string.IsNullOrEmpty(foodGroup) || r.Ingredients.Any(i => i.FoodGroup.Equals(foodGroup, StringComparison.OrdinalIgnoreCase))) &&
                (!maxCalories.HasValue || r.Ingredients.Sum(i => i.Calories) <= maxCalories.Value)).ToList();

            return filteredRecipes;
        }

        public void UpdateRecipe(Recipe recipe)
        {
            var existingRecipe = GetRecipe(recipe.Name);
            if (existingRecipe != null)
            {
                Recipes.Remove(existingRecipe);
                Recipes.Add(recipe);
                SortRecipesByName(); // Ensure sorting after update
            }
        }

        public List<Recipe> GetAllRecipes()
        {
            return Recipes.ToList();
        }
    }
}
