
    public struct Ingrediant
    {
        public Ingrediant(int quantity, string unit)
        {
            Quantity = quantity;
            Unit = unit;
        }

        private int Quantity { get; }
        private string Unit { get; }

        public override string ToString() => $"({X}, {Y})";
    



void Create()
    {
        // Lambda expression to parse an integer from user input
        Func<int> quantity = () =>
        {
            while (true)
            {
                Console.Write("Please select the ingrediant unit of measure: ");
                string unit = Console.ReadLine();
                switch (unit)
                {
                    case "1":
                        break;                    
                    case "2":
                        break;                    
                    case "3s":
                        break;
                    default:
                        break;
                }




                Console.Write("Enter the ingrediant quantity: ");
                string input = Console.ReadLine();
                Ingrediant NewIngrediant = new Ingrediant(input);

                if (int.TryParse(input, out int result))
                    return result;
                else if (result.Equals("x"))
                {
                    Console.WriteLine("Cancelling");
                }

                else
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        };
    }
}
