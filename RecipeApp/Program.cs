using System;
using System.Collections.Generic;
using System.Linq.Expressions;



    class Recipe
    {
        public string RecipeName { get; set; }
        public string Description { get; set; }
        public string[][] Ingredients { get; set; }

        public Recipe(string recipeName)
        {
            RecipeName = recipeName;
            Ingredients = new string[0][];
        }

        public void AddIngredients()
        {
            Console.WriteLine("Add Ingredients");

            int numIngredients = Ingredients.Length;

            // Create a new array with space for one more ingredient
            string[][] newIngredients = new string[numIngredients + 1][];

            // Copy existing ingredients to the new array
            for (int i = 0; i < numIngredients; i++)
            {
                newIngredients[i] = Ingredients[i];
            }

            // Add the new ingredient
            while (true)
            {
                string name = GetNameIngredient();
                int quantity = GetQuantityIngredient();
                string unit = GetUnitIngredient();

                newIngredients[numIngredients] = new string[] { name, quantity.ToString(), unit };

                Console.WriteLine("Add another ingredient? (Y/N)");
                string input = Console.ReadLine().ToUpper();
                if (input != "Y")
                    break;
            }

            // Update the Ingredients array
            Ingredients = newIngredients;
        }
  
        private string GetUnitIngredient()
    {
        // Implement the logic for selecting the unit here
        return "";
    }

    private int GetQuantityIngredient()
    {
        // Implement the logic for getting the quantity here
        return 0;
    }

    private string GetNameIngredient()
    {
        Console.WriteLine("Enter ingredient name:");
        return Console.ReadLine();
    }
}


class Program
{
    static void Main()
    {
        List<Recipe> recipes = new List<Recipe>();
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Recipe Storage App");
            Console.WriteLine("1. Add a Recipe");
            Console.WriteLine("2. View All Recipes");
            Console.WriteLine("3. Search for a Recipe");
            Console.WriteLine("4. Exit");

            Console.Write("\nEnter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Add a Recipe");
                    Console.Write("Enter recipe name:");            
                    string recipeName = Console.ReadLine();
                    Recipe recipe = new Recipe(Commit(recipeName, "Name:"));
                    recipes.Add(recipe);
                    Console.WriteLine("Recipe created successfully!");
                    recipe.AddIngredients();
                    break;

                case "2":
                    Console.WriteLine("View All Recipes");
                        Console.WriteLine("Please select a recipe to see more information about it. \n(To select a recipe, enter the corresponding number and press enter.)");
                        for (int r = 0; r < recipes.Count; r++)
                        {
                            Console.WriteLine($"{r + 1}.________________ \n");
                            Console.WriteLine($"Recipe Name: {recipes[r].RecipeName}");
                            Console.WriteLine($"____________________\n.\n.");

                            for (int i = 0; i < recipes[r].Ingredients.Length; i++)
                            {
                                Console.WriteLine($"{recipes[r].Ingredients[i][2]} {recipes[r].Ingredients[i][1]} of {recipes[r].Ingredients[i][0]}");
                            }
                        }
                        Console.ReadLine();
                    break;
                case "3":
                    Console.WriteLine("Search for a Recipe");
                    // Add logic to search for a recipe
                    break;
                case "4":
                    Console.WriteLine("Exiting...");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }            
        }     
    }

    static string Commit(string recipeName, string variable)
    {
        bool confirmInput = false;
        while (!confirmInput)
        {
            Console.WriteLine($"Save {variable} {recipeName}?");
            bool input = Confirm();

            if (input)
            {
                Console.WriteLine($"{variable} {recipeName} [SAVED!]");
                confirmInput = true;
                return recipeName;
            }
            else if (!input)
            {
                Console.WriteLine($"Please enter another {variable}.");
                recipeName = Console.ReadLine();
                Console.WriteLine($"Save {variable}?");
                confirmInput = Confirm();
            }

            else
            {
                Console.WriteLine("Invalid Option");
                confirmInput = false;
            }
        }
        return recipeName;
    }

    private static bool Confirm()
    {     
            Console.WriteLine("(Y/N)");
            string input = Console.ReadLine().ToUpper();
            return input == "Y";    
    }
}