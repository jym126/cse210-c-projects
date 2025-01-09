using System;
using System.Collections.Generic;
using System.Linq;

class NumberListProgram
{
    static void Main()
    {
        List<int> numbers = new List<int>();
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (true)
        {
            Console.Write("Enter number: ");
            int input = int.Parse(Console.ReadLine());

            if (input == 0)
            {
                break;
            }

            numbers.Add(input);
        }

        // Core requirements
        int sum = numbers.Sum();
        double average = numbers.Average();
        int max = numbers.Max();

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");

        // Stretch Challenge
        int? smallestPositive = numbers.Where(n => n > 0).OrderBy(n => n).FirstOrDefault();
        List<int> sortedNumbers = numbers.OrderBy(n => n).ToList();

        if (smallestPositive.HasValue)
        {
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }
        else
        {
            Console.WriteLine("There are no positive numbers in the list.");
        }

        Console.WriteLine("The sorted list is:");
        foreach (int number in sortedNumbers)
        {
            Console.WriteLine(number);
        }
    }
}
