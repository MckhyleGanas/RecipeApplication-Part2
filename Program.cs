using System;
using System.Collections.Generic;

namespace RecipeApp
{
    class Recipe
    {
        public string Name { get; set; } 
        public List<Ingredient> Ingredients { get; set; }       //Lists used in part 2
        public List<string> Steps { get; set; }

        public Recipe(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();   //Lists used for ingredient class
            Steps = new List<string>();
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public void AddStep(string step)
        {
            Steps.Add(step);
        }

        public void DisplayRecipe()
        {
            Console.WriteLine($"Recipe: {Name}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in Ingredients)
            {
                Console.WriteLine($"- {ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
            }
            Console.WriteLine("Steps:");
            for (int i = 0; i < Steps.Count; i++)       //Each step incremented by 1 depending on how many steps user inputs
            {
                Console.WriteLine($"{i + 1}. {Steps[i]}");
            }
        }

        public void ScaleRecipe(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }

        public void ResetQuantities()
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity = ingredient.OriginalQuantity;
            }
        }

        public double CalculateTotalCalories()
        {
            double totalCalories = 0;
            foreach (var ingredient in Ingredients)
            {
                totalCalories += ingredient.Calories;
            }
            return totalCalories;
        }
    }

    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double OriginalQuantity { get; set; }
        public string Unit { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }

        public Ingredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            OriginalQuantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }

    class Program
    {
        static List<Recipe> recipes = new List<Recipe>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Recipe App");
                Console.WriteLine("1. Add Recipe");
                Console.WriteLine("2. View Recipes");
                Console.WriteLine("3. Clear All Recipes");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddRecipe();
                        break;
                    case "2":
                        ViewRecipes();
                        break;
                    case "3":
                        ClearData();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select options between 1-4.");
                        break;
                }
            }
        }

        static void AddRecipe()
        {
            Console.Write("Enter recipe name: "); //Enter the name of your recipe
            string name = Console.ReadLine();

            Recipe recipe = new Recipe(name);

            Console.Write("Enter the number of ingredients: "); //Enter the number of ingredients your recipe requires
            int numIngredients = int.Parse(Console.ReadLine());

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Enter details for ingredients {i + 1}:"); //Enter the name/calories/food group/quantity of you ingredients
                Console.Write("Name: ");
                string ingredientName = Console.ReadLine();
                Console.Write("Quantity: ");        // Enter quantity of ingredients e.g 3 eggs, 2 cups of milk
                double quantity = double.Parse(Console.ReadLine());
                Console.Write("Unit(mg,g,kg): ");
                string unit = Console.ReadLine();
                Console.Write("Calories: ");    //Calory count of recipe. Warning message is shown if calory intake is more than 300
                int calories = int.Parse(Console.ReadLine());
                Console.Write("Food Group: ");     //Ingredient food group e.g dairy
                string foodGroup = Console.ReadLine();

                Ingredient ingredient = new Ingredient(ingredientName, quantity, unit, calories, foodGroup);
                recipe.AddIngredient(ingredient);
            }

            Console.Write("Enter the number of steps: ");
            int numSteps = int.Parse(Console.ReadLine());       //Recipe steps e.g Step 1:..., Step:2...

            for (int i = 0; i < numSteps; i++)
            {
                Console.Write($"Enter step {i + 1}: ");
                string step = Console.ReadLine();
                recipe.AddStep(step);
            }

            recipes.Add(recipe);
            Console.WriteLine("Recipe successfully added.");
        }

        static void ViewRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes found."); //If user input does not meet parameters, error message is shown
                return;
            }

            Console.WriteLine("Recipes:");  //Add recipe
            recipes.Sort((r1, r2) => string.Compare(r1.Name, r2.Name));
            for (int i = 0; i < recipes.Count; i++) 
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            Console.Write("Enter the recipe number to view: ");  //View previously added recipes
            int recipeNumber = int.Parse(Console.ReadLine());

            if (recipeNumber > 0 && recipeNumber <= recipes.Count)
            {
                Recipe recipe = recipes[recipeNumber - 1];
                recipe.DisplayRecipe();
                double totalCalories = recipe.CalculateTotalCalories();
                Console.WriteLine($"Total Calories: {totalCalories}");  //Total calory count of the ingredients/recipe. If the calory amount exceeds 300, the below message is shown

                if (totalCalories > 300)
                {
                    Console.WriteLine("Warning: Total calories exceeds 300!");
                }

                Console.WriteLine();
                Console.WriteLine("Recipe Options:");
                Console.WriteLine("1. Scale Recipe");
                Console.WriteLine("2. Reset Quantities");
                Console.WriteLine("3. Back to Recipe List");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {                               // Enter a choice between scaling, resetting or returning to your recipe book
                    case "1":                   // If invalid option entered, error message is shown
                        ScaleRecipe(recipe);
                        break;
                    case "2":
                        ResetQuantities(recipe);
                        break;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Returning to Recipe List.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid recipe number.");
            }
        }

        static void ScaleRecipe(Recipe recipe)
        {
            Console.WriteLine("Recipe Scaling:");
            Console.WriteLine("1. Scale by 0.5 (half)");
            Console.WriteLine("2. Scale by 2 (double)");
            Console.WriteLine("3. Scale by 3 (triple)");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)         // User inpur desired scaling factor. If user inputs incorrect scaling measurment, error message is shown
            {
                case "1":
                    recipe.ScaleRecipe(0.5);
                    break;
                case "2":
                    recipe.ScaleRecipe(2);
                    break;
                case "3":
                    recipe.ScaleRecipe(3);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Recipe scaling canceled.");
                    return;
            }

            Console.WriteLine("Recipe successfully scaled.");
            recipe.DisplayRecipe();
        }

        static void ResetQuantities(Recipe recipe)
        {
            recipe.ResetQuantities();       //Reset quatnties of recipe (scaling)
            Console.WriteLine("Quantities reset to its original values.");
            recipe.DisplayRecipe();
        }

        static void ClearData()
        {
            recipes.Clear();        // Clears all the added recipe's in the application
            Console.WriteLine("All recipes has been cleared.");
        }
    }
}
 

