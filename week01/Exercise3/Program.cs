using System;

class MagicNumberGuessing
{
    static void Main()
    {
        bool playAgain = true;

        while (playAgain)
        {
            Random random = new Random();
            int magicNumber = random.Next(1, 101); // Generate random number between 1 and 100
            int userGuess = 0;
            int attempts = 0;

            Console.WriteLine("Welcome to the Magic Number Game!");
            Console.WriteLine("I have picked a random number between 1 and 100.");
            Console.WriteLine("Can you guess what it is?");

            while (userGuess != magicNumber)
            {
                Console.Write("Enter your guess: ");

                try
                {
                    userGuess = int.Parse(Console.ReadLine());
                    attempts++;

                    if (userGuess < magicNumber)
                    {
                        Console.WriteLine("Too low! Try again.");
                    }
                    else if (userGuess > magicNumber)
                    {
                        Console.WriteLine("Too high! Try again.");
                    }
                    else
                    {
                        Console.WriteLine($"Congratulations! You guessed the magic number {magicNumber} in {attempts} attempts.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            Console.Write("Do you want to play again? (yes/no): ");
            string response = Console.ReadLine().Trim().ToLower();
            playAgain = response == "yes";

            if (!playAgain)
            {
                Console.WriteLine("Thanks for playing! Goodbye!");
            }
        }
    }
}