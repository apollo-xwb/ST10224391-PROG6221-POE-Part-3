﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10224391_PROG6221_POE_Part_3
{
    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        public int CaloriesPerServing { get; set; }
        public string FoodGroup { get; set; }
        public double ScaleFactor { get; private set; }
        public double TotalCalories
        {
            get { return Ingredients.Sum(i => i.Calories); }
        }
        public delegate void CaloriesExceededEventHandler(string recipeName, double totalCalories);
        public event CaloriesExceededEventHandler CaloriesExceeded;

        public Recipe(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
            ScaleFactor = 1.0; // Default scale factor
        }

        public Recipe(string name, string foodGroup) : this(name)
        {
            FoodGroup = foodGroup;
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
            CheckCalories();
        }

        public void AddStep(string step)
        {
            Steps.Add(step);
        }

        public void AddSteps(List<string> steps)
        {
            Steps.AddRange(steps);
        }

        public Recipe ScaleRecipe(double scaleFactor)
        {
            if (scaleFactor <= 0)
                throw new ArgumentException("Scale factor must be greater than 0.");

            Recipe scaledRecipe = new Recipe(this.Name + " (Scaled)", this.FoodGroup); // Keep the food group the same
            foreach (var ingredient in Ingredients)
            {
                Ingredient scaledIngredient = new Ingredient(ingredient.Name, ingredient.Quantity * scaleFactor, ingredient.Unit, ingredient.Calories * scaleFactor, ingredient.FoodGroup);
                scaledRecipe.AddIngredient(scaledIngredient);
            }
            scaledRecipe.AddSteps(this.Steps); // Copy steps
            scaledRecipe.CaloriesPerServing = (int)(this.CaloriesPerServing * scaleFactor); // Scale calories per serving
            return scaledRecipe;
        }

        private void CheckCalories()
        {
            double totalCalories = Ingredients.Sum(i => i.Calories);
            if (totalCalories > 300)
            {
                CaloriesExceeded?.Invoke(Name, totalCalories);
            }
        }
    }
}
