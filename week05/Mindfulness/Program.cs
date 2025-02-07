using System;
using System.Collections.Generic;
using System.Threading;

abstract class Activity
{
    protected int Duration;
    
    public void Start()
    {
        Console.Clear();
        Console.WriteLine($"Starting {this.GetType().Name}...");
        Console.WriteLine(GetDescription());
        Console.Write("Enter duration in seconds: ");
        Duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        ShowSpinner(3);
        Run();
        End();
    }

    protected abstract string GetDescription();
    protected abstract void Run();
    
    private void End()
    {
        Console.WriteLine("Great job! You completed the activity.");
        Console.WriteLine($"You spent {Duration} seconds on {this.GetType().Name}.");
        ShowSpinner(3);
    }

    protected void ShowSpinner(int seconds)
    {
        char[] spinner = new[] { '|', '/', '-', '\\' };
        for (int i = 0; i < seconds * 4; i++)
        {
            Console.Write($"\r{spinner[i % spinner.Length]} ");
            Thread.Sleep(250);
        }
        Console.WriteLine();
    }
}

class BreathingActivity : Activity
{
    protected override string GetDescription() => "This activity helps you relax by guiding your breathing.";

    protected override void Run()
    {
        int elapsed = 0;
        while (elapsed < Duration)
        {
            Console.Write("Breathe in:    ");
            BreathingAnimation(3);
            Console.Write("Breathe out:   ");
            BreathingAnimation(3);
            elapsed += 6;
        }
    }

    private void BreathingAnimation(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write("⬤ ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

class ReflectionActivity : Activity
{
    private static readonly string[] Prompts =
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };
    private static readonly string[] Questions =
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?"
    };

    protected override string GetDescription() => "This activity helps you reflect on moments of strength and resilience.";

    protected override void Run()
    {
        Random random = new Random();
        Console.WriteLine(Prompts[random.Next(Prompts.Length)]);
        ShowSpinner(3);

        int elapsed = 0;
        while (elapsed < Duration)
        {
            Console.WriteLine(Questions[random.Next(Questions.Length)]);
            ShowSpinner(5);
            elapsed += 5;
        }
    }
}

class ListingActivity : Activity
{
    private static readonly string[] Prompts =
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?"
    };

    protected override string GetDescription() => "This activity helps you list the positive aspects of your life.";

    protected override void Run()
    {
        Random random = new Random();
        Console.WriteLine(Prompts[random.Next(Prompts.Length)]);
        ShowSpinner(3);
        
        List<string> items = new List<string>();
        int elapsed = 0;
        while (elapsed < Duration)
        {
            Console.Write("Enter an item: ");
            items.Add(Console.ReadLine());
            elapsed += 2;
        }
        Console.WriteLine($"You listed {items.Count} items.");
    }
}

class GratitudeActivity : Activity
{
    private static readonly string[] Prompts =
    {
        "What are three things you are grateful for today?",
        "Who in your life are you most grateful for?",
        "What recent experience made you feel grateful?"
    };

    protected override string GetDescription() => "This activity helps you focus on gratitude and appreciation.";

    protected override void Run()
    {
        Random random = new Random();
        Console.WriteLine(Prompts[random.Next(Prompts.Length)]);
        ShowSpinner(3);
        
        List<string> gratitudeList = new List<string>();
        int elapsed = 0;
        while (elapsed < Duration)
        {
            Console.Write("Enter something you are grateful for: ");
            gratitudeList.Add(Console.ReadLine());
            elapsed += 2;
        }
        Console.WriteLine($"You listed {gratitudeList.Count} things you are grateful for.");
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Select an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Gratitude Activity");
            Console.WriteLine("5. Exit");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();
            Activity activity = choice switch
            {
                "1" => new BreathingActivity(),
                "2" => new ReflectionActivity(),
                "3" => new ListingActivity(),
                "4" => new GratitudeActivity(),
                "5" => null,
                _ => null
            };
            
            if (activity == null) break;
            activity.Start();
        }
    }
}
