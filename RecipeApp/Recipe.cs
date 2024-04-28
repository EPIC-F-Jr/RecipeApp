using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    public class Recipe
    {
        private String title;
        private List<Ingredient> ingredients;
        private List<String> steps;

        public Recipe(String title)
        {
            this.title = title;
            this.ingredients = new List<Ingredient>();
            this.steps = new List<String>();
        }

        public void addIngredient((string name, int quantity, string unit) ingredientVals)
        {
            Ingredient ingredient = new Ingredient(ingredientVals.name, ingredientVals.quantity, ingredientVals.unit );
            this.ingredients.Add(ingredient);
        }

        public void addStep(String description)
        {
            steps.Add(description);
        }

        public String getTitle()
        {
            return this.title;
        }

        public List<Ingredient> getIngredients()
        {
            return ingredients;
        }

        public List<String> getSteps()
        {
            return steps;
        }

        public void setTitle(string? v)
        {
            this.title = v;
        }

        public class Ingredient
        {
            private String name;
            private double quantity;
            private String unit;

            public Ingredient(String name, double quantity, String unit)
            {
                this.name = name;
                this.quantity = quantity;
                this.unit = unit;
            }

            public String getName()
            {
                return name;
            }

            public double getQuantity()
            {
                return quantity;
            }

            public String getUnit()
            {
                return unit;
            }
        }
    }
}