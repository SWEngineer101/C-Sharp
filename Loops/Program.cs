using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loops
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // For Loop
            for(int i = 0; i < 5; i++)
            {
                Console.WriteLine($"For Loop i:  {i}");
            }

            // For Each Loop
            int[] intArray = {0, 1, 2, 3, 4, 5};
            foreach (int item in intArray)
            {
                Console.WriteLine($"For Each Loop intArray: {item}");
            }

            // While Loop
            int counter = 0;
            while(counter < 5)
            {
                Console.WriteLine($"While Loop counter = {counter}");
                counter++;
            }


            // Read Line
            Console.ReadLine();
        }
    }
}
