using System;

class Program
{
    static void Main(string[] args)
    {
        // Ask for grade percentage
        Console.Write("What is your grade percentage? ");
        string input = Console.ReadLine();
        int grade = int.Parse(input);

        // Determine the letter grade
        string gradeLetter = "";
        string gradeSign = "";

        if (grade >= 90)
        {
            gradeLetter = "A";
            if (grade % 10 >= 7)
                gradeSign = "+";
            else if (grade % 10 < 3)
                gradeSign = "-";
        }
        else if (grade >= 80)
        {
            gradeLetter = "B";
            if (grade % 10 >= 7)
                gradeSign = "+";
            else if (grade % 10 < 3)
                gradeSign = "-";
        }
        else if (grade >= 70)
        {
            gradeLetter = "C";
            if (grade % 10 >= 7)
                gradeSign = "+";
            else if (grade % 10 < 3)
                gradeSign = "-";
        }
        else if (grade >= 60)
        {
            gradeLetter = "D";
            if (grade % 10 >= 7)
                gradeSign = "+";
            else if (grade % 10 < 3)
                gradeSign = "-";
        }
        else
        {
            gradeLetter = "F";
            gradeSign = "";
        }
        if (gradeLetter == "A" && grade >= 100) 
        {
            gradeSign = "";
        }
        if (gradeLetter == "F") 
        {
            gradeSign = "";
        }
        Console.WriteLine($"Your grade is {gradeLetter}{gradeSign}");
    }
}
