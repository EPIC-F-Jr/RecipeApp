using System;
using System.Collections.Generic;


class Ingrediant
{
    public Ingrediant(int quantity)
    {
        this.Quantity = quantity;
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

        switch (choice)
        {
            case "1":
                Console.WriteLine("You selected Teaspoon");
                break;
            case "2":
                Console.WriteLine("You selected Tablespoon");
                break;
            case "3":
                Console.WriteLine("You selected Fluid ounce");
                break;
            case "4":
                Console.WriteLine("You selected Cup");
                break;
            case "5":
                Console.WriteLine("You selected Pint");
                break;
            case "6":
                Console.WriteLine("You selected Quart");
                break;
            case "7":
                Console.WriteLine("You selected Gallon");
                break;
            case "8":
                Console.WriteLine("You selected Milliliter");
                break;
            case "9":
                Console.WriteLine("You selected Liter");
                break;
            case "10":
                Console.WriteLine("You selected Ounce");
                break;
            case "11":
                Console.WriteLine("You selected Pound");
                break;
            case "12":
                Console.WriteLine("You selected Gram");
                break;
            case "13":
                Console.WriteLine("You selected Kilogram");
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }

        UnitOfMeasure unit = new UnitOfMeasure();
    }
    public int Quantity { get; set; }
    // You can add more properties like name, unit, etc., if needed

    struct UnitOfMeasure
    {
        public string Name { get; }
        public double BaseAmount { get; }

        public UnitOfMeasure(string name, double baseAmount)
        {
            Name = name;
            BaseAmount = baseAmount;
        }

        public double ConvertToBase(double amount)
        {
            return amount * BaseAmount;
        }

        public double ConvertFromBase(double baseAmount)
        {
            return baseAmount / BaseAmount;
        }
    }
}
class Recipe
{
    public Recipe(string recipeName)
    {
        this.recipeName = recipeName;
        this.ingrediants = new List<Ingrediant>();
    }

    public string recipeName { get; set; }
    private List<Ingrediant> ingrediants { get; set; }

    internal void AddIngrediants()
    {
        Console.WriteLine("Add Ingredients");
        bool validInput = false;
        int quantity = 0;

        while (!validInput)
        {
            Console.WriteLine("Enter ingredient quantity:");
            string input = Console.ReadLine();

            try
            {
                quantity = int.Parse(input);
                validInput = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }

        Ingrediant ingrediant = new Ingrediant(quantity);
        ingrediants.Add(ingrediant);
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
                    recipe.AddIngrediants();
                    break;

                case "2":
                    Console.WriteLine("View All Recipes");
                    foreach (Recipe r in recipes)
                    {
                        Console.WriteLine($"Recipe Name: {r.recipeName}, \nIngredients: \n{r.recipeName}");
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