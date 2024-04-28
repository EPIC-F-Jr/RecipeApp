using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace RecipeApp.res
{
    internal class About
    {
        internal void DisplayInformation()
        {
            WriteLine("Recipe Keeping App");
            WriteLine("Version: 1.0");
            WriteLine("Developer: Nsovo E. Mphande");
            WriteLine("Description: This app allows users to store and manage their recipes.");
            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.White;
            WriteLine("Press any key to continue...");
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;
            ReadKey(true);
        }
    }
}
