using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // One line comment
            // two line comment

            /*
              
             This is a long comment.

            */

            // Integer - Whole Number
            // Can range from -2,147,483,648 to 2,147,483,647
            // Size 4 Bytes
            int myInt = 10;

            // Double - Floating Point Numbers
            // Can store 15 decimal digits
            // Size 8 Bytes
            double myDouble = 2.234523452345D;

            // Long - Whole Number can range from 
            // -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807
            // Size 8 Bytes
            long myLong = 30000000L;

            // Float - Fractional Numbers
            // Can store 6-7 decimal digits
            // Size 4 Bytes
            float myFloat = 12.234234F;

            // Boolean True || False
            // Size 1 Bit
            bool isYoutubeCool = true;

            // Character
            // Store single character/letter sourrounded by single quotes
            // Size 2 Bytes
            char myChar = 'a';

            // String
            // Stroe sequence of characters surrounded by double quotes
            // Size 2 Bytes per character
            string myString = "This is our custom string";


            // Here we are printing all of our variables
            Console.WriteLine("This is my C# Console Program");
            Console.WriteLine($"myInt = {myInt}");
            Console.WriteLine($"myDouble = {myDouble}");
            Console.WriteLine($"myLong = {myLong}");
            Console.WriteLine($"myFloat = {myFloat}");
            Console.WriteLine($"isYoutubeCool = {isYoutubeCool}");
            Console.WriteLine($"myChar = {myChar}");
            Console.WriteLine($"myString = {myString}");


            // This keeps the Console from closing after executing code
            Console.WriteLine("Press any key to exit..");
            Console.ReadLine();
        }
    }
}
