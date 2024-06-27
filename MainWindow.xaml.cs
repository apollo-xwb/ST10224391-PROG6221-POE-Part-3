using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace ST10224391_PROG6221_POE_Part_3
{
    public partial class MainWindow : Window
    {
        private RecipeManager recipeManager;
        private ObservableCollection<Ingredient> currentIngredients;
        private ObservableCollection<string> currentSteps;

        public MainWindow()
        {
            InitializeComponent();
            recipeManager = new RecipeManager();
            currentIngredients = new ObservableCollection<Ingredient>();
            currentSteps = new ObservableCollection<string>();
            IngredientsListBox.ItemsSource = currentIngredients;
            StepsListBox.ItemsSource = currentSteps;
            RecipeListBox.ItemsSource = recipeManager.Recipes.Select(r => r.Name); // Bind only recipe names
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            string name = IngredientNameTextBox.Text;
            if (double.TryParse(QuantityTextBox.Text, out double quantity) &&
                double.TryParse(CaloriesTextBox.Text, out double calories))
            {
                string unit = UnitTextBox.Text;
                string foodGroup = FoodGroupTextBox.Text;
                Ingredient ingredient = new Ingredient(name, quantity, unit, calories, foodGroup);
                currentIngredients.Add(ingredient);

                // Check total calories after adding the ingredient
                double totalCalories = currentIngredients.Sum(i => i.Calories);
                if (totalCalories > 300)
                {
                    MessageBox.Show($"Warning: Calories for '{name}' exceed 300 (Total: {totalCalories}).");
                }

                // Clear input fields after adding ingredient
                IngredientNameTextBox.Clear();
                QuantityTextBox.Clear();
                UnitTextBox.Clear();
                CaloriesTextBox.Clear();
                FoodGroupTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please enter valid numerical values for quantity and calories.");
            }
        }

        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            string step = StepTextBox.Text;
            if (!string.IsNullOrEmpty(step))
            {
                currentSteps.Add(step);
                StepTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Step cannot be empty.");
            }
        }

        private void SaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = RecipeNameTextBox.Text;
            if (string.IsNullOrEmpty(recipeName))
            {
                MessageBox.Show("Please enter a recipe name.");
                return;
            }

            Recipe recipe = new Recipe(recipeName);
            foreach (var ingredient in currentIngredients)
            {
                recipe.AddIngredient(ingredient);
            }
            foreach (var step in currentSteps)
            {
                recipe.AddStep(step);
            }

            recipeManager.AddRecipe(recipe);
            MessageBox.Show("Recipe saved successfully.");

            // Clear fields and lists after saving recipe
            RecipeNameTextBox.Clear();
            currentIngredients.Clear();
            currentSteps.Clear();

            // Update RecipeListBox to display recipe names
            RecipeListBox.ItemsSource = recipeManager.Recipes.Select(r => r.Name);
        }

        private void ClearSteps_Click(object sender, RoutedEventArgs e)
        {
            currentSteps.Clear();
        }

        private void RecipeListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RecipeListBox.SelectedItem is string recipeName) // Handle selection of recipe name
            {
                var recipe = recipeManager.GetRecipe(recipeName);
                DisplayRecipeDetails(recipe);
            }
        }

        private void DisplayRecipeDetails(Recipe recipe)
        {
            if (recipe != null)
            {
                StringBuilder details = new StringBuilder();
                details.AppendLine($"Recipe Name: {recipe.Name}");
                details.AppendLine("Ingredients:");
                foreach (var ingredient in recipe.Ingredients)
                {
                    details.AppendLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} - {ingredient.Calories} calories ({ingredient.FoodGroup})");
                }
                details.AppendLine("Steps:");
                foreach (var step in recipe.Steps)
                {
                    details.AppendLine(step);
                }
                RecipeDetailsTextBlock.Text = details.ToString();
            }
        }

        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeListBox.SelectedItem is string recipeName) // Handle selection of recipe name
            {
                if (double.TryParse(ScaleFactorTextBox.Text, out double scaleFactor))
                {
                    try
                    {
                        Recipe selectedRecipe = recipeManager.GetRecipe(recipeName);
                        Recipe scaledRecipe = selectedRecipe.ScaleRecipe(scaleFactor);
                        recipeManager.AddRecipe(scaledRecipe); // Add scaled recipe to manager
                        RecipeListBox.SelectedItem = scaledRecipe.Name; // Select the scaled recipe name in the list
                        DisplayRecipeDetails(scaledRecipe); // Display scaled recipe details
                        MessageBox.Show("Recipe scaled successfully.");
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid scale factor.");
                }
            }
            else
            {
                MessageBox.Show("Please select a recipe to scale.");
            }
        }
    }
}
