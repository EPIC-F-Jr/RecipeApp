using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
     class Recipe
    {

        public Recipe(string? recipeName)
        {
            this.recipeName = recipeName;
        }

        public required string recipeName { get; set; }

    }
}
