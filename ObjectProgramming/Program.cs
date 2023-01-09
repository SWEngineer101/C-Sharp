using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectProgramming
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Company One Info
            Company one = new Company();
            one.name = "Apple";
            one.sector = "Information Technology";
            one.yearFounded = "1976";

            // Company Two Info
            Company two = new Company();
            two.name = "Microsoft";
            two.sector = "Information Technology";
            two.yearFounded = "1975";

            // Company Three Info
            Company three = new Company();
            three.name = "McDonalds";
            three.sector = "Consumer Discretionary";
            three.yearFounded = "1955";

            // Print out to console
            Console.WriteLine($"Name: {one.name}\nSector: {one.sector}\nFounded: {one.yearFounded}");
            Console.WriteLine($"Name: {two.name}\nSector: {two.sector}\nFounded: {two.yearFounded}");
            Console.WriteLine($"Name: {three.name}\nSector: {three.sector}\nFounded: {three.yearFounded}");

            // Stop from closing
            Console.ReadLine();
        }
    }
}
