using RecipeApp;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

    class Recipe
    {
        public string recipeName { get; set; }
        public string Description;
        public string[][] Ingredients;

        public Recipe(string recipeName)
        {
            this.recipeName = recipeName;
            Ingredients = new string[0][]; // Initialize Ingredients as an empty array
        }
    public void EditIngredients()
    {
        Console.WriteLine("Edit Ingredients");

        // Display current ingredients
        for (int i = 0; i < Ingredients.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Ingredients[i][0]}, {Ingredients[i][1]} {Ingredients[i][2]}");
        }

        bool editIngredient = true;
        while (editIngredient)
        {
            Console.Write("Enter the number of the ingredient to edit or 0 to exit: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int index) && index >= 1 && index <= Ingredients.Length)
            {
                // Get new ingredient details
                string name = GetNameIngredient();
                int quantity = GetQuantityIngredient();
                string unit = GetUnitIngredient();

                // Update the ingredient at the specified index
                Ingredients[index - 1] = new string[] { name, quantity.ToString(), unit };

                Console.WriteLine("Ingredient updated successfully!");
            }
            else if (index == 0)
            {
                editIngredient = false;
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }
    public void AddIngredients()
    {
        Console.WriteLine("Add Ingredients");

        bool addIngredient = true;
        while (addIngredient)
        {
            string name = GetNameIngredient();
            int quantity = GetQuantityIngredient();
            string unit = GetUnitIngredient();

            // Create a new ingredient array
            string[] newIngredient = new string[] { name, quantity.ToString(), unit };

            // Resize the Ingredients array and add the new ingredient
            Array.Resize(ref Ingredients, Ingredients.Length + 1);
            Ingredients[Ingredients.Length - 1] = newIngredient;

            Console.WriteLine("Add another ingredient? (Y/N)");
            string input = Console.ReadLine().ToUpper();
            if (input != "Y")
            {
                addIngredient = false;
            }
        }
    }

    private string GetUnitIngredient()
    {
        Console.WriteLine("Select a category:");
        Console.WriteLine("1. Volume");
        Console.WriteLine("2. Weight");

        Console.Write("Enter your choice: ");
        string categoryChoice = Console.ReadLine();

        switch (categoryChoice)
        {
            case "1":
                Console.WriteLine("Select a volume unit:");
                Console.WriteLine("1. Teaspoon: tsp");
                Console.WriteLine("2. Tablespoon: tbsp or T");
                Console.WriteLine("3. Fluid ounce: fl oz");
                Console.WriteLine("4. Cup: cup");
                Console.WriteLine("5. Pint: pt");
                Console.WriteLine("6. Quart: qt");
                Console.WriteLine("7. Gallon: gal");
                Console.WriteLine("8. Milliliter: ml");
                Console.WriteLine("9. Liter: l");
                break;
            case "2":
                Console.WriteLine("Select a weight unit:");
                Console.WriteLine("1. Ounce: oz");
                Console.WriteLine("2. Pound: lb");
                Console.WriteLine("3. Gram: g");
                Console.WriteLine("4. Kilogram: kg");
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }

        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();
        string unit = "";
        switch (choice)
        {
            case "1":
                Console.WriteLine("You selected Teaspoon");
                unit = "tsp";
                break;
            case "2":
                Console.WriteLine("You selected Tablespoon");
                unit = "tbsp";
                break;
            case "3":
                Console.WriteLine("You selected Fluid ounce");
                unit = "fl oz";
                break;
            case "4":
                Console.WriteLine("You selected Cup");
                unit = "cup";
                break;
            case "5":
                Console.WriteLine("You selected Pint");
                unit = "pt";
                break;
            case "6":
                Console.WriteLine("You selected Quart");
                unit = "qt";
                break;
            case "7":
                Console.WriteLine("You selected Gallon");
                unit = "gal";
                break;
            case "8":
                Console.WriteLine("You selected Milliliter");
                unit = "ml";
                break;
            case "9":
                Console.WriteLine("You selected Liter");
                unit = "l";
                break;
            case "10":
                Console.WriteLine("You selected Ounce");
                unit = "oz";
                break;
            case "11":
                Console.WriteLine("You selected Pound");
                unit = "lb";
                break;
            case "12":
                Console.WriteLine("You selected Gram");
                unit = "g";
                break;
            case "13":
                Console.WriteLine("You selected Kilogram");
                unit = "kg";
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
        return unit;
    }
    private int GetQuantityIngredient()
    {
        int quantity = 0;
        bool validInput = false;
        while (!validInput)
        {
            Console.WriteLine("Enter ingredient quantity:");
            string ingrediantQuantity = Console.ReadLine();

            try
            {
                quantity = int.Parse(ingrediantQuantity);
                validInput = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }
        return quantity;
    }
    private string GetNameIngredient()
    {
        string name = "";
        bool isConfirmed = false;

        while (!isConfirmed)
        {
            Console.WriteLine("Enter ingredient name:");
            name = Console.ReadLine();

            Console.WriteLine($"You entered: {name}");
            Console.WriteLine("Are you satisfied with this name? (Y/N)");

            string confirmation = Console.ReadLine().ToUpper();

            if (confirmation == "Y")
            {
                isConfirmed = true;
            }
        }

        return name;
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
            string choice = MainMenu();

            switch (choice)
            {
                case "1":
                    recipes.Add(NewRecipe());
                    recipes[recipes.Count - 1].AddIngredients();
                    break;
                case "2":
                    ViewRecipes(recipes);
                    break;
                case "3":
                    Console.WriteLine("Search for a Recipe");
                    // Add logic to search for a recipe.
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

    private static void ViewRecipes(List <Recipe> recipes)
    {
        Console.WriteLine("View All Recipes");
        Console.WriteLine("Please select a recipe to see more information about it. \n(To select a recipe, enter the corresponding number and press enter.)");
        for (int r = 0; r < recipes.Count; r++)
        {
            Console.WriteLine($"{r + 1}.________________ \n");
            Console.WriteLine($"Recipe Name: {recipes[r].recipeName}");
            Console.WriteLine($"____________________\n.\n.");
        }
        Console.WriteLine("\nPress select a recipe or press enter to continue...");
        bool validInput = false;
        while (!validInput)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (char.IsDigit(key.KeyChar))
            {
                if (int.TryParse(key.KeyChar.ToString(), out int viewRecipeChoice))
                {
                    if (viewRecipeChoice >= 1 && viewRecipeChoice <= recipes.Count)
                    {
                        Console.WriteLine($"Recipe Number: {viewRecipeChoice}\nRecipe Name: {recipes[viewRecipeChoice - 1].recipeName}");
                        foreach (string[] ingredient in recipes[viewRecipeChoice - 1].Ingredients)
                        {
                            Console.WriteLine($"{ingredient[1]} {ingredient[2]} of {ingredient[0]}");
                        }
                        Console.WriteLine("Enter: Main Menu -- Edit Recipe(1):Edit Recipe.");
                        string userInput = Console.ReadLine();

                        switch (userInput)
                        {
                            case "":
                                Console.WriteLine("Back to main menu");
                                validInput = true;
                                break;
                            case "1":
                                // Edit recipe logic here
                                Console.WriteLine("Edit Recipe");
                                DisplayRecipeAndEdit(recipes[viewRecipeChoice - 1]);
                                break;
                            default:
                                Console.WriteLine("Invalid input. Please try again.");
                                break;
                        }
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine("Back to main menu");
                validInput = true;
            }
        }
    }

    private static void DisplayRecipeAndEdit(Recipe recipe)
    {
            Console.WriteLine($"Recipe Name: {recipe.recipeName}");
            Console.WriteLine("Ingredients:");
            for (int i = 0; i < recipe.Ingredients.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {recipe.Ingredients[i][1]} {recipe.Ingredients[i][2]} of {recipe.Ingredients[i][0]}");
            }

            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Edit Recipe");
            Console.WriteLine("2. Add Ingredient");
            Console.WriteLine("3. Go back to main menu");
            string userInput;
            Console.Write("Enter your choice: ");
            userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    // Edit recipe logic
                    recipe.EditIngredients();
                    break;
                case "2":
                    // Add ingredient logic
                    recipe.AddIngredients();
                    break;
                case "3":
                    Console.WriteLine("Going back to the main menu...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

    private static Recipe NewRecipe()
    {
        Console.WriteLine("Add a Recipe");
        Console.Write("Enter recipe name:");
        string recipeName = Console.ReadLine();
        Recipe newRecipe = new Recipe(Commit(recipeName, "Name:"));
        Console.WriteLine("Recipe created successfully!");
        return newRecipe;
    }

    private static string MainMenu()
    {
        Console.Clear();
        Console.WriteLine("Recipe Storage App");
        Console.WriteLine("1. Add a Recipe");
        Console.WriteLine("2. View All Recipes");
        Console.WriteLine("3. Search for a Recipe");
        Console.WriteLine("4. Exit");

        Console.Write("\nEnter your choice: ");
        return Console.ReadLine();
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