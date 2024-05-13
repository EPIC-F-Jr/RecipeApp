using RecipeApp.res;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static RecipeApp.Recipe;
using static System.Console;

namespace RecipeApp
{
    class App
    {
        List<Ingredient>resetIngredients;
        List<Recipe> recipes = new List<Recipe>();
        Recipe newRecipe;
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
            do
            {
                switch (selectedIndex)
                {
                    case 0: // New Recipe
                        Clear();
                        newRecipe = new Recipe(NameRecipe()); // Create a new Recipe object
                        ModifyRecipe(newRecipe);
                        break;
                    case 1:
                        Clear();
                        DisplayRecipe();
                        break;
                    case 2:
                        Clear();
                        DisplayAboutApp();
                        break;
                    case 3:
                        ExitApp();
                        break;
                }
            } while (selectedIndex != 3);

            WriteLine("Press any key to exit...");
            ReadKey(true);
        }

        private void ModifyRecipe(Recipe newRecipe)
        {
            string ingredientsString = GetIngredientsString(newRecipe);
            
            string[] options = { "Done", "Add Ingredient", "Rename Recipe", "Rename Ingredient", "Cancel/Delete Recipe" };
            Menu menu = new Menu($"Modify Recipe: {newRecipe.getTitle()} \nCurrent Ingredients: \n{ingredientsString}", options);

            int selectedIndex;
            do
            {
                Console.Clear();
                selectedIndex = menu.Run();

                switch (selectedIndex)
                {
                    case 0: // Done
                        if (!recipes.Contains(newRecipe))
                            {
                            recipes.Add(newRecipe);
                        } else {
                            
                            recipes[recipes.IndexOf(newRecipe)] = newRecipe;
                        }
                        WriteLine("RECIPE ADDED!");
                        RunMainMenu();
                        break;
                    case 1: // Add Ingredient
                        AddIngredient(newRecipe);
                        ModifyRecipe(newRecipe);
                        break;
                    case 2: // Rename Recipe
                        newRecipe.setTitle(NameRecipe());
                        ModifyRecipe(newRecipe);
                        break;
                    case 3: // Rename Ingredient
                        RenameIngredients(newRecipe);
                        break;
                    case 4: // Delete Recipe
                        if (recipes.Contains(newRecipe))
                        {
                            recipes.Remove(newRecipe);
                            WriteLine("RECIPE DELTED!");
                        }
                        else {
                            WriteLine("New Recipe CANCELED!");
                            RunMainMenu();
                        }
                        return;
                }
            } while (selectedIndex != 0); // Continue until "Done" is selected

            // Display the added ingredients
            foreach (Ingredient ingredient in newRecipe.getIngredients())
            {
                Console.WriteLine($"Added ingredient: {ingredient.getName()}, {ingredient.getQuantity()} {ingredient.getUnit()}");
            }
            resetIngredients = newRecipe.getIngredients();
        }

        private void RenameIngredients(Recipe newRecipe)
        {
            string[] options = new string[newRecipe.getIngredients().Count()+1];
            if (newRecipe.getIngredients().Count != 0)
            {
                foreach (Ingredient ingredient in newRecipe.getIngredients())
                {
                    WriteLine($"{ingredient.getName()}");
                    for (int i = 0; i < newRecipe.getIngredients().Count(); i++)
                    {
                        options[i] = newRecipe.getIngredients()[i].getName();
                        options[options.Length - 1] = "Cancel";
                    }
                }
            }
            else { options[0] = "Cancel"; }

            Menu menu = new Menu("Rename INGREDIENT\n", options);

            int selectionIndex = menu.Run();

            if (selectionIndex != options.Length - 1)
            {
                ModifyIngredient(newRecipe.getIngredients()[options.Length],newRecipe);
                ModifyRecipe(newRecipe);
            }
            else {
                ModifyRecipe(newRecipe);
            }
            resetIngredients = newRecipe.getIngredients();
        }

        private void ModifyIngredient(Ingredient ingredient, Recipe recipe)
        {
            string ingredientDetails = ($"EDIT INGREDIENT: {ingredient.getName()} \nQUANTITY: {ingredient.getQuantity()} {ingredient.getUnit()} of {ingredient.getName()}" +
                $"\nFood Group: {ingredient.getFoodGroup()}" +
                $"\nCALORIES: {ingredient.getCalories()}");
            string[] options = { "Rename", "Add Step", "Delete Step", "Select Food Group", "Specify Calories", "Delete Ingredient", "Back" };
            Menu menu = new Menu($"{ingredientDetails}\nMenu: ", options);
            int selectedIndex;
            do
            {
                Console.Clear();
                selectedIndex = menu.Run();

                switch (selectedIndex)
                {
                    case 0: // Rename
                        WriteLine($"Renaming INGREDIENT {ingredient.getName()}\n(Please enter the new name and press ENTER)");
                        string newName = ReadLine();
                        string[] newOptions = { "Save", "Cancel" };
                        options = newOptions;
                        menu = new Menu($"Save name: {newName}", options);
                        selectedIndex = menu.Run();
                        switch (selectedIndex)
                        {
                            case 0:
                                ingredient.setName(newName);
                                break;
                            case 1:
                                break;
                        }
                        break;
                    case 1: // Add Step
                        WriteLine("Adding STEP." +
                            "\nPlease enter instructions (Press ENTER to continue.):");
                        string step = ReadLine();
                        string[] opts = { "Save", "Cancel" };
                        menu = new Menu($"Save STEP: \n{step}\n", opts);
                        selectedIndex = menu.Run();
                        switch (selectedIndex)
                        {
                            case 0:
                                ingredient.addStep(step);
                                break;
                            case 1:
                                break;
                        }
                        break;

                    case 2: // Delete Step
                        string [] stepsOptions = ingredient.getSteps().ToArray();
                        menu = new Menu("Please select a step to delete.", stepsOptions);
                        selectedIndex = menu.Run();

                        break;

                                        
                            
                    case 3: // Select Food Group
                        string[] foodGroups = { "Vegetables", "Fruits", "Grains", "Proteins", "Dairy", "Fats and Oils" };
                        menu = new Menu("Please select a FOOD GROUP for this ingredient.", foodGroups);
                        selectedIndex = menu.Run();
                        ingredient.setFoodGroup(foodGroups[selectedIndex]);
                        WriteLine($"FOODGROUP {foodGroups[selectedIndex]} SELECTED.");
                        ModifyIngredient(ingredient, recipe);
                        break;
                    case 4: // Specify Calories
                        WriteLine("Please enter the calories of this ingredient.");
                        int calories = Read();
                        string[] CaloriesOptions = { "Save", "Cancel"};
                        menu = new Menu($"Are you sure about these INGREDIENT CALORIES?. {calories}", CaloriesOptions);
                        selectedIndex = menu.Run();
                        switch (selectedIndex)
                        {
                            case 0:
                                ingredient.setCalories(calories);
                                break;
                            case 1:
                                ModifyIngredient(ingredient, recipe);
                                break;
                        }                      
                        break;

                    case 5: // Delete Ingredient
                        string[] optionList = {"Delete", "Cancel"};
                        menu = new Menu("Are you sure you want to delete this INGREDIENT?", optionList);
                        selectedIndex = menu.Run();
                        switch (selectedIndex)
                        {
                            case 0:
                                if (recipe.getIngredients().Contains(ingredient)) {
                                    recipe.getIngredients().Remove(ingredient);
                                    WriteLine("INGREDIENT DELETED\n(Press ENTER to continue.)");
                                    Read();
                                } else { WriteLine("INGREDIENT not found!"); Read(); };
                                break;
                            case 1:
                                break;
                        }
                        break;
                    case 6: // Back
                        ModifyRecipe(newRecipe);
                        return;
                }
            } while (selectedIndex != 0); // Continue until "Done" is selected
            resetIngredients = newRecipe.getIngredients();
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
            List<string> recipeTitles = recipes.Select(r => r.getTitle()).ToList();
            recipeTitles.Add("Cancel");
            Menu recipeMenu = new Menu("Select a recipe:", recipeTitles.ToArray());
            int selectedIndex = recipeMenu.Run();
            
            if (selectedIndex >= 0 && selectedIndex < recipes.Count)
            {
                Recipe selectedRecipe = recipes[selectedIndex];
                string ingredientsString = selectedRecipe.getTitle() + ":\n" + "--------------------------\n" + GetIngredientsString(selectedRecipe) + "\n--------------------------\n";
                /*Console.WriteLine($"• Recipe: {selectedRecipe.getTitle()}\n");
                    WriteLine("--------------------------");
                foreach (Ingredient ingredient in selectedRecipe.getIngredients())
                {
                    Console.WriteLine($"- {ingredientsString}");
                }
                    WriteLine("--------------------------"); */
                string[] options = { "Scale Recipe", "Edit Recipe", "Edit Ingredients", "Delete Recipe", "Back", "Main Menu" };
                Menu menu = new Menu($"{ingredientsString}", options);
                selectedIndex = menu.Run();
                switch (selectedIndex)
                {
                    case 0: // Scale Recipe
                        ScaleRecipe(selectedRecipe);
                        break;
                    case 1: // Edit Recipe
                        ModifyRecipe(selectedRecipe);
                        break;
                    case 2: // Edit Ingredients
                        string[] newOptions = new string[selectedRecipe.getIngredients().Count+1];
                        for (int i = 0; i < selectedRecipe.getIngredients().Count;)
                        {
                            newOptions[i] = selectedRecipe.getIngredients()[i].getName();
                            for (int j = 0; j < 1; j++) { newOptions[selectedRecipe.getIngredients().Count] = "Cancel."; };
                            i++;
                        }
                        options = newOptions;
                        Menu editIngredientMenu = new Menu($"Select an {selectedRecipe.getTitle()} ingredient to EDIT:\n", options);
                        selectedIndex = editIngredientMenu.Run();
                        if (selectedIndex == options.Length)
                        {
                            ModifyRecipe(selectedRecipe);
                        }
                        else
                        {
                            ModifyIngredient(selectedRecipe.getIngredients()[selectedIndex], selectedRecipe);
                        }
                        break;
                    case 3: // Delete Recipe
                        WriteLine($"RECIPE {selectedRecipe.getTitle()} DELETED");
                        recipes.Remove(newRecipe);
                        break;
                    case 4: // Back
                        DisplayRecipe();
                        break;
                    case 5: // Main Menu
                        RunMainMenu();
                        return;
                }
                Console.ReadKey(true);
                RunMainMenu();
            }
            else if (selectedIndex == recipeTitles.Count - 1) { WriteLine("Running main menu"); RunMainMenu(); };
        }

        private void ScaleRecipe(Recipe selectedRecipe)
        {
            string ingredientsString = selectedRecipe.getTitle() + ":\n" + "--------------------------\n" + GetIngredientsString(selectedRecipe) + "\n--------------------------\n";

            string[] options = { "Increase Serving", "Decrease Serving", "Reset Serving", "Back" };
            Menu menu = new Menu($"{ingredientsString}", options);
            int selectedIndex = menu.Run();
            switch (selectedIndex)
            {
                case 0:
                    selectedRecipe.Multiplier(selectedIndex);
                    ScaleRecipe(selectedRecipe);
                    break;
                case 1:
                    selectedRecipe.Multiplier(selectedIndex);
                    ScaleRecipe(selectedRecipe);
                    break;
                case 2:
                    selectedRecipe.Multiplier(selectedIndex);
                    ScaleRecipe(selectedRecipe);
                    break;
                case 3:
                    DisplayRecipe();
                    break;
            }
        }
            

        private void AddIngredient(Recipe newRecipe)
        {
             // Add Ingredient
             // Display existing ingredients                        
            Ingredient newIngredient = CreateIngredient();                                
            newRecipe.getIngredients().Add(newIngredient);
        }
        public string GetIngredientsString(Recipe recipe)
        {
            StringBuilder builder = new StringBuilder();
            int index = 1;
            if (recipe != null && recipe.getIngredients() != null && recipe.getIngredients().Count() != 0)
            {
                foreach (Ingredient ingredient in recipe.getIngredients())
                {
                    builder.Append($"{index}. {ingredient.getQuantity()} {ingredient.getUnit()} of {ingredient.getName()}, \n");
                    index++;
                }
            }
            else { builder.Append("None so far, Add some?"); }
        
            // Remove the trailing comma and space
            if (builder.Length >= 2)
            {
                builder.Remove(builder.Length - 2, 2);
            }

            return builder.ToString();
        }
        static string NameRecipe()
        {
            Console.WriteLine("Enter the recipe name (press Enter to finish):");
            string recipeName = ReadLine();


            Console.WriteLine(); // Move to the next line

            // Use Menu class to allow the user to save or clear the name
            string[] options = { "Save Name", "Clear Name" };

            Menu menu = new Menu($"Recipe name: {recipeName}  \n\nSelect an option: ", options);

            int selectedIndex = menu.Run();

            if (selectedIndex == 0)
            {
                Clear();
                return recipeName;
            }
            else
            {
                Clear();
                return NameRecipe(); // Recursive call to restart the process
            }
        }
        Ingredient CreateIngredient()
        {

            string ingredientName;
            int ingredientQuantity;
            string ingredientUnit;
            string confirmation;

                Console.WriteLine("Enter the ingredient name:");
                ingredientName = Console.ReadLine();

                Console.WriteLine("Enter the ingredient quantity:");
                while (!int.TryParse(Console.ReadLine(), out ingredientQuantity))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }

                Console.WriteLine("Enter the ingredient unit:");
                ingredientUnit = GetUnitOfMeasure();
                Console.WriteLine($"Selected unit of measure: {ingredientUnit}");
            Clear();

                Console.WriteLine($"You entered '{ingredientName}' as the ingredient name, {ingredientQuantity} as the quantity, and '{ingredientUnit}' as the unit.");
                Console.WriteLine("Press Enter to add another ingredient or any other key to return to the menu.");
            ReadKey();
            return new Ingredient(ingredientName, ingredientQuantity, ingredientUnit);
        }
        string GetUnitOfMeasure()
        {
            string[] unitOptions = { "grams", "kilograms", "milliliters", "liters", "teaspoons", "tablespoons", "cups", "pieces" };
            Menu unitMenu = new Menu("Select a unit of measure:", unitOptions);
            int selectedIndex = unitMenu.Run();
            return unitOptions[selectedIndex];
        }
    }
}
            
        
    

