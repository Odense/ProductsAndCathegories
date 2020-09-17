using System;

namespace ProductsAndCategories
{
    class Program
    {
        protected static int origRow;
        protected static int origCol;

        private static void Main(string[] args)
        {
            while (true)
            {
                PrintMainMenu();
                var choice = Console.ReadLine();
                if (choice != "1") break;
                PrintList();
            }
        }

        private static void PrintMainMenu()
        {
            Console.Clear();

            Console.WriteLine("What do you want? (Enter appropriate number)");
            Console.WriteLine("1. Get list of products with their categories\n2. Exit");
            Console.Write("Your choice: ");
        }

        private static void PrintList()
        {
            Console.Clear();

            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            try
            {
                var jsonData = BusinessLogic.GetData("http://tester.consimple.pro");
                var productsModel = BusinessLogic.DeserializeResponse(jsonData);
                var products = BusinessLogic.GetViewModel(productsModel);

                WriteAt("Product name", 0, 0);
                WriteAt("Category name\n", 25, 0);
                var i = 0;
                var j = 0;

                foreach (var product in products)
                {
                    WriteAt(product.ProductName, 0, ++i);
                    WriteAt(product.CategoryName, 25, ++j);
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("\n\nTo get to the previous menu press any key... ");
            Console.ReadKey();
        }

        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}