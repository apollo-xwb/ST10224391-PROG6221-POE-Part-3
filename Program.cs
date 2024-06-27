// using System;

// public class Program
// {
//     static void Main(string[] args)
//     {
//         Console.ForegroundColor = ConsoleColor.Cyan;
//         Console.WriteLine("Welcome to the #1 Recipe Manager!");
//         Console.ResetColor();

//         RecipeManager manager = new RecipeManager();
//         bool running = true;

//         while (running)
//         {
//             DisplayMainMenu();

//             Console.Write("Choose an option: ");
//             string choice = Console.ReadLine();

//             switch (choice)
//             {
//                 case "1":
//                     AddRecipe(manager);
//                     break;
//                 case "2":
//                     manager.DisplayAllRecipes();
//                     break;
//                 case "3":
//                     ViewRecipe(manager);
//                     break;
//                 case "4":
//                     running = false;
//                     break;
//                 default:
//                     Console.ForegroundColor = ConsoleColor.Red;
//                     Console.WriteLine("Invalid choice. Please try again.");
//                     Console.ResetColor();
//                     break;
//             }
//         }

//         Console.ForegroundColor = ConsoleColor.Green;
//         Console.WriteLine("Thank you for using the #1 Recipe App. See you soon!");
//         Console.ResetColor();
//     }

//     static void DisplayMainMenu()
//     {
//         Console.ForegroundColor = ConsoleColor.Yellow;
//         Console.WriteLine("\n=========================");
//         Console.WriteLine("         MAIN MENU        ");
//         Console.WriteLine("=========================");
//         Console.ResetColor();

//         Console.WriteLine("1. Add Recipe");
//         Console.WriteLine("2. List Recipes");
//         Console.WriteLine("3. View Recipe");
//         Console.WriteLine("4. Exit");
//         Console.WriteLine("=========================");
//     }

//     static void AddRecipe(RecipeManager manager)
//     {
//         Console.ForegroundColor = ConsoleColor.Yellow;
//         Console.WriteLine("\n=========================");
//         Console.WriteLine("       ADD NEW RECIPE     ");
//         Console.WriteLine("=========================");
//         Console.ResetColor();

//         Console.Write("Enter recipe name: ");
//         string name = Console.ReadLine();
//         Recipe recipe = new Recipe(name);
//         recipe.CaloriesExceeded += Recipe_CaloriesExceeded;

//         bool addingIngredients = true;
//         while (addingIngredients)
//         {
//             Console.Write("Enter an ingredients name (or type 'done' to finish): ");
//             string ingredientName = Console.ReadLine();
//             if (ingredientName.ToLower() == "done") break;

//             double quantity = 0;
//             while (true)
//             {
//                 Console.Write("Enter a quantity: ");
//                 if (double.TryParse(Console.ReadLine(), out quantity)) break;
//                 else
//                 {
//                     Console.ForegroundColor = ConsoleColor.Red;
//                     Console.WriteLine("Invalid input for a quantity. Please make sure to key in a number.");
//                     Console.ResetColor();
//                 }
//             }

//             Console.Write("Enter unit: ");
//             string unit = Console.ReadLine();

//             double calories = 0;
//             while (true)
//             {
//                 Console.Write("Enter calories: ");
//                 if (double.TryParse(Console.ReadLine(), out calories)) break;
//                 else
//                 {
//                     Console.ForegroundColor = ConsoleColor.Red;
//                     Console.WriteLine("Invalid input for the calories. Please enter a number.");
//                     Console.ResetColor();
//                 }
//             }

//             Console.Write("Enter food group: ");
//             string foodGroup = Console.ReadLine();

//             Ingredient ingredient = new Ingredient(ingredientName, quantity, unit, calories, foodGroup);
//             recipe.AddIngredient(ingredient);
//         }

//         bool addingSteps = true;
//         while (addingSteps)
//         {
//             Console.Write("Enter the steps required (or type 'done' to finish): ");
//             string step = Console.ReadLine();
//             if (step.ToLower() == "done") break;
//             recipe.AddStep(step);
//         }

//         manager.AddRecipe(recipe);
//         Console.ForegroundColor = ConsoleColor.Green;
//         Console.WriteLine("The recipe has been added successfully!");
//         Console.ResetColor();
//     }

//     static void ViewRecipe(RecipeManager manager)
//     {
//         Console.ForegroundColor = ConsoleColor.Yellow;
//         Console.WriteLine("\n=========================");
//         Console.WriteLine("       VIEW RECIPE        ");
//         Console.WriteLine("=========================");
//         Console.ResetColor();

//         Console.Write("Enter the recipe's name to view it: ");
//         string name = Console.ReadLine();
//         manager.DisplayRecipeDetails(name);
//     }

//     private static void Recipe_CaloriesExceeded(string recipeName, double totalCalories)
//     {
//         Console.ForegroundColor = ConsoleColor.Red;
//         Console.WriteLine($"Warning: The recipe '{recipeName}' is over 300 calories (Total: {totalCalories} calories).");
//         Console.ResetColor();
//     }
// }
