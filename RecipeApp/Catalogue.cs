using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    class Catalogue
    {
        private List<Recipe> Recipes;

        public Catalogue(List<Recipe> recipes) { 
        Recipes = recipes;
        }
        public List<Recipe> getCatalogue() {         
            
            return Recipes;
        }
    }
}
