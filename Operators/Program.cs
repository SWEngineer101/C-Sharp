using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Addition Operator
            int myInt = 0;
            //myInt = myInt + 1;
            //myInt += 1;
            myInt++;
            // Print Statement
            Console.WriteLine($"myInt = {myInt}");

            // Subtraction Operator
            int mySubInt = 0;
            //mySubInt = mySubInt - 1;
            mySubInt--;
            // Print Statement
            Console.WriteLine($"mySubInt = {mySubInt}");

            // Multiplication Operator
            int myMultInt = 5;
            //myMultInt = myMultInt * 1;
            myMultInt *= 2;
            Console.WriteLine($"myMultInt = {myMultInt}");

            // Division Operator
            int myDivInt = 25;
            //myDivInt = myDivInt / 5;
            myDivInt /= 5;
            Console.WriteLine($"myDivInt = {myDivInt}");

            // Modulus Operator
            int myModInt = 25;
            //myModInt = myModInt % 5;
            myModInt %= 5;
            Console.WriteLine($"myModInt = {myModInt}");

            // Comparison = Operator
            if (0 == 1)
            {
                Console.WriteLine("True Statement");
            }
            else
            {
                Console.WriteLine("False Statement");
            }

            // Comparison != Operator
            if (0 != 1)
            {
                Console.WriteLine("True Statement");
            }
            else
            {
                Console.WriteLine("False Statement");
            }

            // Comparison < Operator
            // Can use <=
            if (0 < 1)
            {
                Console.WriteLine("True Statement");
            }
            else
            {
                Console.WriteLine("False Statement");
            }

            // Comparison > Operator
            // Can use >=
            if (0 > 1)
            {
                Console.WriteLine("True Statement");
            }
            else
            {
                Console.WriteLine("False Statement");
            }

            // Logical AND Operator
            if(0 < 1 && 1 == 1)
            {
                Console.WriteLine("Logical AND True Statement");
            }
            else
            {
                Console.WriteLine("Logical AND False Statement");
            }

            // Logical OR Operator
            if (0 == 1 || 1 == 0)
            {
                Console.WriteLine("Logical OR True Statement");
            }
            else
            {
                Console.WriteLine("Logical OR False Statement");
            }

            // Logical NOT Operator
            if (!(1 == 1)) // if(opposite(true))
            {
                Console.WriteLine("Logical NOT True Statement");
            }
            else
            {
                Console.WriteLine("Logical NOT False Statement");
            }


            // Console Read Line
            Console.Read();
        }
    }
}
