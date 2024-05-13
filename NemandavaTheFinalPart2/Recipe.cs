using NemandavaTheFinalPart2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NemandavaTheFinalPart2
{
    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        public event CaloriesNotification CaloriesExceeded; // Event declaration

        public int TotalCalories //method to calulate total calories
        {
            get { return Ingredients.Sum(ingredient => ingredient.Calories); }
        }

        public Recipe(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
        }

        public void AddIngredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            Ingredients.Add(new Ingredient { Name = name, Quantity = quantity, OriginalQuantity = quantity, Calories = calories, FoodGroup = foodGroup });
        }

        public void AddStep(string step)
        {
            Steps.Add(step);
        }

        public void DisplayRecipe()
        {
            Console.WriteLine($"Recipe: {Name}");
            Console.WriteLine("Ingredients:");
            foreach (Ingredient ingredient in Ingredients)
            {
                // Colorize the output for ingredients based on their food group
                string colorCode = "";
                switch (ingredient.FoodGroup.ToLower())
                {
                    case "protein":
                        colorCode = "\u001b[31m"; // Red
                        break;
                    case "vegetables":
                        colorCode = "\u001b[32m"; // Green
                        break;
                    case "carbohydrates":
                        colorCode = "\u001b[33m"; // Yellow
                        break;
                    case "dairy":
                        colorCode = "\u001b[34m"; // Blue
                        break;
                    case "fruits":
                        colorCode = "\u001b[35m"; // Magenta
                        break;
                    default:
                        colorCode = "\u001b[0m"; // Reset to default color
                        break;
                }

                string calorieExplanation = GetCalorieExplanation(ingredient.Calories);
                Console.WriteLine($"{colorCode}- {ingredient.Name}: {ingredient.Quantity} {ingredient.Unit} ({ingredient.Calories} calories) ({ingredient.FoodGroup}) - {calorieExplanation}\u001b[0m"); // Reset color after output
            }
            Console.WriteLine("Steps:");
            for (int i = 0; i < Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Steps[i]}");
            }
            Console.WriteLine($"Total Calories: {TotalCalories}");

            // Check if the event has subscribers and invoke it if total calories exceed 300
            if (CaloriesExceeded != null && TotalCalories > 300)
            {
                CaloriesExceeded(TotalCalories);

            }
        }
        private string GetCalorieExplanation(int calories)
        {
            if (calories < 100)
            {
                return "Low calorie";
            }
            else if (calories >= 100 && calories <= 300)
            {
                return "Moderate calorie";
            }
            else
            {
                return "High calorie";
            }
        }
    }
}