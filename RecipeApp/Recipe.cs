using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    public class Recipe : IComparable<Recipe>
    {
        private String title;
        private int multiplier;
        private List<Ingredient> ingredients;
        public delegate void CaloriesExceededEventHandler();
        public event CaloriesExceededEventHandler CaloriesExceeded;

        public Recipe(String title)
        {
            this.title = title;
            ingredients = new List<Ingredient>();
            multiplier = 1;
        }

        public void addIngredient(Ingredient ingredient)
        {
            this.ingredients.Add(ingredient);
        }

        private void CheckTotalCalories()
        {
            int totalCalories = ingredients.Sum(i => i.getCalories());
            if (totalCalories > 300)
            {
                CaloriesExceeded?.Invoke();
            }
        }

        public String getTitle()
        {
            return this.title;
        }

        public List<Ingredient> getIngredients()
        {
            CheckTotalCalories();
            return ingredients;
        }
        public void setIngredients(List<Ingredient> Ingredients)
        {
            ingredients = Ingredients;
            CheckTotalCalories();

        }

        public void setTitle(string? v)
        {
            title = v;
        }
        public void Multiplier(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    this.multiplier++;
                    foreach (Ingredient i in ingredients) { 
                    i.setMultiplier(multiplier);
                    }
                    break;
                case 1:
                    if (multiplier == 1)
                    {
                        this.multiplier = 1;
                        foreach (Ingredient i in ingredients) { i.setMultiplier(multiplier); }
                    }
                    else
                    {
                        this.multiplier--;
                        foreach (Ingredient i in ingredients)
                        {
                            i.setMultiplier(multiplier);
                        }
                    }
                    break;
                case 2:
                    multiplier = 1; foreach (Ingredient i in ingredients) { i.setMultiplier(multiplier); }
                    break;
            }
        }

        public int CompareTo(Recipe other)
        {
            if (other == null) return 1;
            return this.title.CompareTo(other.title);
        }

        public class Ingredient
        {
            private String name;
            private double quantity;
            private String unit;
            private List<String> steps;
            private int multiplier;
            private string foodGroup;
            private int calories;

            public Ingredient(String name, double quantity, String unit)
            {
                this.name = name;
                this.quantity = quantity;
                this.unit = unit;
                this.steps = new List<String>();
                this.multiplier = 1;
                this.foodGroup = "NULL";
            }
            public String getName()
            {
                return name;
            }
            public void setName(string Name)
            {
                this.name = Name;
            }
            public void setMultiplier(int Multiplier) {
                this.multiplier = Multiplier;
            }

            public double getQuantity()
            {
                return quantity * multiplier;
            }
            public void setFoodGroup(string FoodGroup)
            {
                this.foodGroup = FoodGroup;
            }
            public void setCalories(int Calories)
            {
                this.calories = Calories;
            }
            public string getFoodGroup()
            {
               return this.foodGroup;
            }
            public int getCalories()
            {
                return this.calories;
            }

            public String getUnit()
            {
                return unit;
            }
            public List<String> getSteps()
            {
                return steps;
            }

            public void addStep(String description)
            {
                steps.Add(description);
            }
        }
    }
}