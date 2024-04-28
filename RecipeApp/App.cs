using RecipeApp.res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static RecipeApp.Recipe;
using static System.Console;

namespace RecipeApp
{
    class App
    {
        List<Recipe> recipes = new List<Recipe>();
        public void Start()
        {
            Title = "Recipe App";
            RunMainMenu();
        }
        private void RunMainMenu()
        {
            WriteLine("App starting...");
            WriteLine("Press any key.");
            ConsoleKeyInfo keyPressed = ReadKey();

            /*if (keyPressed.Key == ConsoleKey.Enter)
            {

            } else if (keyPressed.Key == ConsoleKey.UpArrow) {

            } else { 
            
            }*/

            string prompt = @" ███████████                      ███                    
░░███░░░░░███                    ░░░                     
 ░███    ░███   ██████   ██████  ████  ████████   ██████ 
 ░██████████   ███░░███ ███░░███░░███ ░░███░░███ ███░░███
 ░███░░░░░███ ░███████ ░███ ░░░  ░███  ░███ ░███░███████ 
 ░███    ░███ ░███░░░  ░███  ███ ░███  ░███ ░███░███░░░  
 █████   █████░░██████ ░░██████  █████ ░███████ ░░██████ 
░░░░░   ░░░░░  ░░░░░░   ░░░░░░  ░░░░░  ░███░░░   ░░░░░░  
                                       ░███              
                                       █████             
                                      ░░░░░              
   █████████                                             
  ███░░░░░███                                            
 ░███    ░███  ████████  ████████                        
 ░███████████ ░░███░░███░░███░░███                       
 ░███░░░░░███  ░███ ░███ ░███ ░███                       
 ░███    ░███  ░███ ░███ ░███ ░███                       
 █████   █████ ░███████  ░███████                        
░░░░░   ░░░░░  ░███░░░   ░███░░░                         
               ░███      ░███                            
               █████     █████                           
              ░░░░░     ░░░░░                            
Welcome to the Recipe App!
(Use the arrow keys to navigate the menu.)";

            string[] options = { "New Recipe", "Display Recipe", "About", "Exit" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Clear();
                    NewRecipe();
                    RunMainMenu();
                    break;
                case 1:
                    Clear();
                    DisplayRecipe();
                    RunMainMenu();
                    break;
                case 2:
                    Clear();
                    DisplayAboutApp();
                    RunMainMenu();
                    break;
                case 3:
                    ExitApp();
                    break;
            }

            WriteLine("Press any key to exit...");
            ReadKey(true);
        }

        private void ExitApp()
        {
            WriteLine("Press any key to exit...");
            Environment.Exit(0);
        }
        private void DisplayAboutApp()
        {
            About about = new About();
            about.DisplayInformation();
            Clear();
        }
        private void DisplayRecipe()
        {
            Console.WriteLine("Select a recipe:");
            foreach (Recipe r in recipes)
            {
                Console.WriteLine($"{r.getTitle()}");
            }

        }

        private void NewRecipe()
        {
            Recipe newRecipe = new Recipe(CreateRecipe());
            Clear();
            newRecipe.addIngredient(CreateIngredient(newRecipe));
            recipes.Add(newRecipe);

        }

        static string CreateRecipe()
        {
            Console.WriteLine("Enter the recipe name (press Enter to finish):");
            StringBuilder recipeNameBuilder = new StringBuilder();

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true); // ReadKey intercepts the key press
                if (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Backspace)
                {
                    recipeNameBuilder.Append(keyInfo.KeyChar); // Append the key to the recipeNameBuilder
                    Console.Write(keyInfo.KeyChar); // Display the character
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && recipeNameBuilder.Length > 0)
                {
                    recipeNameBuilder.Length--; // Remove the last character if backspace is pressed
                    Console.Write("\b \b"); // Move the cursor back and erase the character
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Move to the next line

            // Use Menu class to allow the user to save or clear the name
            string[] options = { "Save Name", "Clear Name" };
            Menu menu = new Menu($"Recipe name: {recipeNameBuilder.ToString()} \n\nSelect an option: ", options);
            int selectedIndex = menu.Run();

            if (selectedIndex == 0)
            {
                return recipeNameBuilder.ToString();
            }
            else
            {
                Console.Clear();
                return CreateRecipe(); // Recursive call to restart the process
            }
        }
        private (string, int, string) CreateIngredient(Recipe newRecipe)
        {
            string ingredientName = "";
            int ingredientQuantity = 0;
            string ingredientUnit = "";
            string confirmation;

            Console.WriteLine($"Recipe name: {newRecipe.getTitle()}");
            string[] options = {"Done", "Add Ingredient", "Rename Recipe", "Cancel" };
            Menu menu = new Menu($"Recipe name: {newRecipe.getTitle()} \n\nSelect an option:", options);

            int selectedIndex = -1;

            do
            {
                if (selectedIndex == -1)
                {
                    Console.Clear();
                    Console.WriteLine($"Recipe name: {newRecipe.getTitle()}");
                    Console.WriteLine("Select an option:");
                }

                selectedIndex = menu.Run(); // Display the options and get the user's choice

                switch (selectedIndex)
                {
                    case 0: // Add Ingredient
                        recipes.Add(newRecipe);
                        Clear();
                        Start();
                        break;                   
                    case 1: // Add Ingredient
                        Clear();
                        Console.WriteLine($"Recipe name: {newRecipe.getTitle()} \n");
                        Console.WriteLine("Enter the ingredient name:");
                        ingredientName = Console.ReadLine();

                        Console.WriteLine("Enter the ingredient quantity:");
                        while (!int.TryParse(Console.ReadLine(), out ingredientQuantity))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid integer.");
                        }

                        Console.WriteLine("Enter the ingredient unit:");
                        ingredientUnit = Console.ReadLine();

                        Console.WriteLine($"You entered '{ingredientName}' as the ingredient name, {ingredientQuantity} as the quantity, and '{ingredientUnit}' as the unit.");
                        Console.WriteLine("Press Enter to add another ingredient or any other key to return to the menu.");

                        if (Console.ReadKey().Key != ConsoleKey.Enter)
                        {
                            selectedIndex = 2; // Return to the menu
                        }
                        break;
                    case 2: // Rename Recipe
                        newRecipe.setTitle(CreateRecipe());
                        Console.WriteLine("Recipe renamed successfully.");
                        CreateIngredient(newRecipe);
                        break;
                    case 3: // Cancel
                        Console.WriteLine("Cancelled.");
                        break;
                }

            } while (selectedIndex != 2); // Repeat until Cancel option is selected

            return (ingredientName, ingredientQuantity, ingredientUnit);
        }
    }
}
