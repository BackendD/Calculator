using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
			while (true)
			{
				System.Console.Write("\nEnter polinomial: f=");
				string[] input = System.Console.ReadLine().Split(',');
				Polinomial p = new Polinomial(input[0]);
				System.Console.Write("The answer is: f=");
				string[] values = new string[input.Length - 1];
				Array.Copy(input, 1, values, 0, values.Length);
				System.Console.WriteLine(p.Answer(values));
			}
        }
    }
}
