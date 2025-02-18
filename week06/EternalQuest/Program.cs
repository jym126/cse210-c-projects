using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

abstract class Goal
{
    public string Name { get; protected set; }
    public int Points { get; protected set; }
    public bool IsComplete { get; set; } // Changed to public to allow setting during loading

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        IsComplete = false;
    }

    public abstract int RecordEvent();
    public abstract string GetProgress();
}

class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) { }
    public override int RecordEvent() { IsComplete = true; return Points; }
    public override string GetProgress() => IsComplete ? "[X]" : "[ ]";

    // Save data specific to SimpleGoal, including IsComplete status
    public string SaveData() => $"SimpleGoal,{Name},{Points},{IsComplete}";
}

class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }
    public override int RecordEvent() => Points;
    public override string GetProgress() => "[∞]";

    // Save data specific to EternalGoal
    public string SaveData() => $"EternalGoal,{Name},{Points}";
}

class ChecklistGoal : Goal
{
    private int TargetCount, Bonus;
    public int CurrentCount { get; set; }

    public ChecklistGoal(string name, int points, int targetCount, int bonus, int currentCount = 0) : base(name, points)
    {
        TargetCount = targetCount;
        Bonus = bonus;
        CurrentCount = currentCount;
    }

    public override int RecordEvent()
    {
        CurrentCount++;
        if (CurrentCount >= TargetCount) { IsComplete = true; return Points + Bonus; }
        return Points;
    }

    public override string GetProgress() => IsComplete ? "[X]" : $"[{CurrentCount}/{TargetCount}]";

    // Save data specific to ChecklistGoal
    public string SaveData() => $"ChecklistGoal,{Name},{Points},{TargetCount},{Bonus},{CurrentCount}";
}

class EternalQuest
{
    private List<Goal> goals = new List<Goal>();
    private int score = 0;

    public void CreateGoal()
    {
        Console.WriteLine("Select goal type:\n1. Simple Goal\n2. Eternal Goal\n3. Checklist Goal");
        string type = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        Console.Write("Enter points for the goal: ");
        if (!int.TryParse(Console.ReadLine(), out int points))
        {
            Console.WriteLine("Invalid points input.\n");
            return;
        }

        switch (type)
        {
            case "1": goals.Add(new SimpleGoal(name, points)); break;
            case "2": goals.Add(new EternalGoal(name, points)); break;
            case "3":
                Console.Write("Enter target count: ");
                if (!int.TryParse(Console.ReadLine(), out int targetCount))
                {
                    Console.WriteLine("Invalid target count input.\n");
                    return;
                }
                Console.Write("Enter bonus points: ");
                if (!int.TryParse(Console.ReadLine(), out int bonus))
                {
                    Console.WriteLine("Invalid bonus points input.\n");
                    return;
                }
                goals.Add(new ChecklistGoal(name, points, targetCount, bonus));
                break;
            default:
                Console.WriteLine("Invalid selection.\n");
                break;
        }
    }

    public void RecordGoal()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals created yet.\n");
            return;
        }

        Console.WriteLine("Select a goal to record:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name}");
        }

        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= goals.Count)
        {
            score += goals[choice - 1].RecordEvent();
            Console.WriteLine($"You earned {goals[choice - 1].Points} points! Total Score: {score}\n");
        }
        else
        {
            Console.WriteLine("Invalid choice.\n");
        }
    }

    public void ShowGoals()
    {
        Console.WriteLine($"Total Score: {score}\n"); // Display the total score clearly

        if (goals.Count == 0)
        {
            Console.WriteLine("No goals created yet.\n");
            return;
        }

        Console.WriteLine("Your goals:");
        foreach (var goal in goals)
            Console.WriteLine($"{goal.GetProgress()} {goal.Name}");
    }

    public void SaveProgress()
    {
        Console.Write("Enter filename to save: ");
        string filename = Console.ReadLine();
        using (StreamWriter sw = new StreamWriter(filename))
        {
            sw.WriteLine(score);
            foreach (var goal in goals)
            {
                if (goal is SimpleGoal simpleGoal)
                    sw.WriteLine(simpleGoal.SaveData());
                else if (goal is EternalGoal eternalGoal)
                    sw.WriteLine(eternalGoal.SaveData());
                else if (goal is ChecklistGoal checklistGoal)
                    sw.WriteLine(checklistGoal.SaveData());
            }
        }
    }

    public void LoadProgress()
    {
        Console.Write("Enter filename to load: ");
        string filename = Console.ReadLine();
        Console.WriteLine();
        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.\n");
            return;
        }
        using (StreamReader sr = new StreamReader(filename))
        {
            // Read the score
            if (!int.TryParse(sr.ReadLine(), out score))
            {
                Console.WriteLine("Invalid score in file.\n");
                return;
            }

            goals.Clear();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                var parts = line.Split(',');
                if (parts.Length < 3) // Ensure there are at least 3 parts (type, name, points)
                {
                    Console.WriteLine($"Invalid line in file: {line}");
                    continue;
                }

                string type = parts[0];
                string name = parts[1];
                if (!int.TryParse(parts[2], out int points))
                {
                    Console.WriteLine($"Invalid points in line: {line}");
                    continue;
                }

                switch (type)
                {
                    case "SimpleGoal":
                        if (parts.Length >= 4 && bool.TryParse(parts[3], out bool isComplete))
                        {
                            var goal = new SimpleGoal(name, points);
                            goal.IsComplete = isComplete; // Restore the IsComplete status
                            goals.Add(goal);
                        }
                        else
                        {
                            Console.WriteLine($"Invalid IsComplete status in line: {line}");
                        }
                        break;

                    case "EternalGoal":
                        goals.Add(new EternalGoal(name, points));
                        break;

                    case "ChecklistGoal":
                        if (parts.Length >= 6 &&
                            int.TryParse(parts[3], out int targetCount) &&
                            int.TryParse(parts[4], out int bonus) &&
                            int.TryParse(parts[5], out int currentCount))
                        {
                            goals.Add(new ChecklistGoal(name, points, targetCount, bonus, currentCount));
                        }
                        else
                        {
                            Console.WriteLine($"Invalid ChecklistGoal data in line: {line}");
                        }
                        break;

                    default:
                        Console.WriteLine($"Unknown goal type in line: {line}");
                        break;
                }
            }
        }
    }

    // Feature 1: Reset Goal Progress
    public void ResetGoalProgress()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals created yet.\n");
            return;
        }

        Console.WriteLine("Select a goal to reset progress:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name}");
        }
        Console.WriteLine();

        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= goals.Count)
        {
            var goal = goals[choice - 1];
            goal.IsComplete = false;

            if (goal is ChecklistGoal checklistGoal)
            {
                checklistGoal.CurrentCount = 0;
            }

            Console.WriteLine("Goal progress reset successfully.\n");
        }
        else
        {
            Console.WriteLine("Invalid choice.\n");
        }
    }

    // Feature 2: Goal Statistics
    public void ShowStatistics()
    {
        int totalGoals = goals.Count;
        int completedGoals = goals.Count(g => g.IsComplete);
        int totalPointsEarned = score;
        double averagePoints = totalGoals > 0 ? (double)totalPointsEarned / totalGoals : 0;

        Console.WriteLine("Goal Statistics:");
        Console.WriteLine($"- Total Goals: {totalGoals}");
        Console.WriteLine($"- Completed Goals: {completedGoals}");
        Console.WriteLine($"- Total Points Earned: {totalPointsEarned}");
        Console.WriteLine($"- Average Points per Goal: {averagePoints:F2}\n");
    }
}

class Program
{
    static void Main()
    {
        EternalQuest quest = new EternalQuest();

        while (true)
        {
            Console.WriteLine("1. Create Goal\n2. Show Goals\n3. Record Event\n4. Save Progress\n5. Load Progress\n6. Reset Goal Progress\n7. Show Statistics\n8. Exit");
            switch (Console.ReadLine())
            {
                case "1": quest.CreateGoal(); break;
                case "2": quest.ShowGoals(); break;
                case "3": quest.RecordGoal(); break;
                case "4": quest.SaveProgress(); break;
                case "5": quest.LoadProgress(); break;
                case "6": quest.ResetGoalProgress(); break;
                case "7": quest.ShowStatistics(); break;
                case "8": return;
                default: Console.WriteLine("Invalid option.\n"); break;
            }
        }
    }
}