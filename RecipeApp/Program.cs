using System;
using System.Collections.Generic;

class Recipe
{
    public Recipe(string recipeName)
    {
        this.recipeName = recipeName;
    }

    public string recipeName { get; set; }
    private List <Ingrediant> ingrediants { get; set; }

    internal void AddIngrediants()
    {
        Ingrediant ingrediant = new Ingrediant();
        ingrediant.Create();
        return ingrediant;        
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
                    Console.Write("Enter recipe name: ");            
                    string input = Console.ReadLine();
                    Recipe recipe = new Recipe(Commit( ref input, "Name: "));

                    Console.Write("Are you sure about this recipe name: ");
                    Console.WriteLine("1. Yes, continue.");
                    Console.WriteLine("2. No, rename the recipe.");
                    int selection = Console.Read();

                    if (selection != null)
                    {
                        Console.Write("[INVALID INPUT] Please confirm using \"1\" or \"2\"..");
                        Console.Write("Are you sure about this recipe name: ");
                        Console.WriteLine("1. Yes, continue.");
                        Console.WriteLine("2. No, rename the recipe.");
                    }
                    else if (selection == 1 || selection == 2)
                    {
                        if (selection == 1)
                        {
                            Recipe recipe = new Recipe(recipeName);
                            Console.WriteLine($"Recipe name: {recipeName}");
                            Console.Write("Add ingrediants to the recipe?");   
                            Console.Write("\"Yes\" [Y] \\ \"No\" [N]");
                            string response = Console.ReadLine();

                            switch (response) 
                            {
                                case "Y":
                                    recipe.AddIngrediants();
                                    break;
                                case "N":
                                    Console.WriteLine();
                                    break;                                
                                default:
                                    Console.WriteLine();
                                    break;
                            }
                            recipes.Add(recipe);
                            Console.WriteLine("Recipe added successfully!");

                        }
                        Console.Write("Enter recipe ingredients: ");
                        string recipeIngredients = Console.ReadLine();
                    }
                    // Commit the new name to the recipe
                    recipes.Add(recipeName, recipeIngredients);
                    Console.WriteLine("Recipe added successfully!");
                    break;



                case "2":
                    Console.WriteLine("View All Recipes");
                    foreach (Recipe r in recipes)
                    {
                        Console.WriteLine($"Recipe Name: {r.recipeName}, Ingredients: {r.recipeIngredients}");
                    }
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

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    static void Commit<T>(T data, T variable)
    {
        bool confirmInput = false;
        while (!confirmInput)
        {
            Console.WriteLine($"Save {variable}?");
            bool input = Confirm();

            if (input)
            {
                Console.WriteLine($"{variable}: {data} [SAVED!]");
                confirmInput = true;
            }
            else if (!input)
            {
                Console.WriteLine($"Please enter another {variable}.");
                var newInput = Console.ReadLine();
                Console.WriteLine($"Save {variable}?");
                confirmInput = Confirm();
            }

            else
            {
                Console.WriteLine("Invalid Option");
            }
        }
    }

    private static bool Confirm()
    {     
            Console.WriteLine("(Y/N)");
            string input = Console.ReadLine().ToUpper();
            return input == "Y";    
    }
}