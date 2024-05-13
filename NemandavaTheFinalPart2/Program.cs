
using NemandavaTheFinalPart2;
using System;


public class Program
{
    private static List<Recipe> recipes = new List<Recipe>();

    public static void Main()
    {
        bool exit = false;
        DisplayMenu(); // Display the menu at the start

        while (!exit)
        {
            Console.Write("Enter your choice from the original menu: ");
            string choice = Console.ReadLine();

            switch (choice.ToLower())
            {
                case "1":
                    CreateRecipe();
                    break;
                case "2":
                    DisplayRecipes();
                    break;
                case "3":
                    DisplayRecipeDetails();
                    break;
                case "4":
                    ScaleRecipe();
                    break;
                case "5":
                    ResetQuantities();
                    break;
                case "6":
                    Console.Write("Enter the number of the recipe to clear: ");
                    if (!int.TryParse(Console.ReadLine(), out int clearIndex) || clearIndex < 1 || clearIndex > recipes.Count)
                    {
                        Console.WriteLine("Invalid recipe number.");
                        break;
                    }
                    ClearAllData(clearIndex - 1); // Adjust for 0-based index
                    break;

                case "7":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            // Display the menu again only if a recipe is added
            if (choice.ToLower() == "1" && recipes.Count > 0)
            {
                DisplayMenu();
            }
        }
    }

    public static void DisplayMenu()
    {
        Console.WriteLine("\n****Original Menu:***");
        Console.WriteLine("*1. Add a New Recipe NB* an unlimited number of recipes can be entered*");
        Console.WriteLine("*2. Display Recipes*");
        Console.WriteLine("*3. View Recipe Details*");
        Console.WriteLine("*4. Scale Recipe*");
        Console.WriteLine("*5. Reset Quantities*");
        Console.WriteLine("*6. Clear All Data*");
        Console.WriteLine("*7. Exit*");
    }

    public static void CreateRecipe()
    {
        Console.Write("\nEnter recipe name: ");
        string name = Console.ReadLine();
        Recipe recipe = new Recipe(name);

        Console.Write("Enter the number of ingredients: ");
        int ingredientCount;
        while (!int.TryParse(Console.ReadLine(), out ingredientCount) || ingredientCount < 1)
        {
            Console.WriteLine("Invalid input. Please enter a valid number greater than 0.");
        }

        int totalCalories = 0; // Track total calories as ingredients are added

        for (int i = 0; i < ingredientCount; i++)
        {
            Console.Write("Enter ingredient name (press Enter to finish): ");
            string ingredientName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ingredientName))
                break;

            Console.Write($"Enter the quantity of {ingredientName}: ");
            double quantity;
            while (!double.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid quantity.");
            }

            Console.Write("Enter the number of calories for this ingredient: ");
            int calories;
            while (!int.TryParse(Console.ReadLine(), out calories) || calories <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid number of calories.");
            }

            totalCalories += calories; // Add calories to the total

            Console.Write($"Enter food group (to select them) for {ingredientName} (e.g., protein, vegetables, carbohydrates, dairy, fruits): ");
            string foodGroup = Console.ReadLine();

            recipe.AddIngredient(ingredientName, quantity, "", calories, foodGroup);

        }

        Console.Write("\nEnter the number of steps: ");
        int stepCount;
        while (!int.TryParse(Console.ReadLine(), out stepCount) || stepCount < 1)
        {
            Console.WriteLine("Invalid input. Please enter a valid number greater than 0.");
        }

        for (int i = 0; i < stepCount; i++)
        {
            Console.Write($"Enter step {i + 1} description: ");
            string stepDescription = Console.ReadLine();
            recipe.AddStep(stepDescription);
        }

        // Display the list of ingredients, steps, and total calories regardless of exceeding 300
        Console.WriteLine("\nIngredients, Steps, and Total Calories:");
        recipe.DisplayRecipe();

        // Check if the total calories exceed 300 and display a warning
        if (totalCalories > 300)
        {
            Console.WriteLine("Warning: Total calories exceed 300!");
        }

        recipes.Add(recipe);
    }

    public static void DisplayRecipeDetails()
    {
        if (recipes.Count == 0)
        {
            Console.WriteLine("No recipes available.");
            return;
        }

        DisplayRecipes();
        Console.Write("\nEnter the number of the recipe to view details: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > recipes.Count)
        {
            Console.WriteLine("Invalid recipe number.");
            return;
        }

        List<Recipe> sortedRecipes = recipes.OrderBy(recipe => recipe.Name).ToList(); // Sort recipes by name
        Recipe selectedRecipe = sortedRecipes[choice - 1];

        selectedRecipe.DisplayRecipe();
    }

    public static void DisplayRecipes()
    {
        List<Recipe> sortedRecipes = recipes.OrderBy(recipe => recipe.Name).ToList(); // Sort recipes by name

        if (sortedRecipes.Count == 0)
        {
            Console.WriteLine("No recipes available.");
            return;
        }

        Console.WriteLine("\u001b[35m\nRecipes:\u001b[0m"); // Pink color for "Recipes:"
        for (int i = 0; i < sortedRecipes.Count; i++)
        {
            Console.WriteLine($"\u001b[35m{i + 1}. {sortedRecipes[i].Name}\u001b[0m"); // Pink color for recipe names
        }
    }


    public static void ScaleRecipe()
    {
        if (recipes.Count == 0)
        {
            Console.WriteLine("No recipes available.");
            return;
        }

        DisplayRecipes();
        Console.Write("\nEnter the number of the recipe to scale: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > recipes.Count)
        {
            Console.WriteLine("Invalid recipe number.");
            return;
        }

        List<Recipe> sortedRecipes = recipes.OrderBy(recipe => recipe.Name).ToList();
        Recipe selectedRecipe = sortedRecipes[choice - 1];

        Console.Write("Enter scaling factor (0.5 for half, 2 for double, 3 for triple): ");
        if (!double.TryParse(Console.ReadLine(), out double factor) || factor <= 0)
        {
            Console.WriteLine("Invalid scaling factor.");
            return;
        }

        // Scale the recipe
        foreach (Ingredient ingredient in selectedRecipe.Ingredients)
        {
            ingredient.Quantity = ingredient.OriginalQuantity * factor; // Scale using OriginalQuantity
        }

        Console.WriteLine("Recipe scaled successfully.");
        selectedRecipe.DisplayRecipe();
    }

    public static void ResetQuantities()
    {
        if (recipes.Count == 0)
        {
            Console.WriteLine("No recipes available.");
            return;
        }

        DisplayRecipes();
        Console.Write("\nEnter the number of the recipe to reset quantities: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > recipes.Count)
        {
            Console.WriteLine("Invalid recipe number.");
            return;
        }

        List<Recipe> sortedRecipes = recipes.OrderBy(recipe => recipe.Name).ToList();
        Recipe selectedRecipe = sortedRecipes[choice - 1];

        // Reset quantities to original values
        foreach (Ingredient ingredient in selectedRecipe.Ingredients)
        {
            ingredient.Quantity = ingredient.OriginalQuantity;
        }

        Console.WriteLine("Quantities reset successfully.");
        selectedRecipe.DisplayRecipe();
    }

    public static void ClearAllData(int index)
    {
        if (index < 0 || index >= recipes.Count)
        {
            Console.WriteLine("Invalid recipe index.");
            return;
        }

        recipes.RemoveAt(index);
        Console.WriteLine("Selected recipe cleared.");
    }

}
