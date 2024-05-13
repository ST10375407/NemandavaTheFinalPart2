using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NemandavaTheFinalPart2
{
    public delegate void CaloriesNotification(int totalCalories);

    public class Ingredient
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public double Quantity { get; set; } // Include quantity property
        public double OriginalQuantity { get; set; } // Add OriginalQuantity property
        public string Unit { get; set; } // Include unit property
        public string FoodGroup { get; set; } //for allocating the food groups 
    }
}