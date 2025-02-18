using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Create a list to store different activities
        List<Activity> activities = new List<Activity>();

        // Add some activities
        activities.Add(new RunningActivity("2025-02-18", 30, 5)); // 5 km running in 30 minutes
        activities.Add(new CyclingActivity("2025-02-18", 45, 20)); // 20 km cycling in 45 minutes
        activities.Add(new SwimmingActivity("2025-02-18", 30, 40)); // 40 laps swimming in 30 minutes

        // Iterate through activities and display their summaries
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
            Console.WriteLine();
        }
    }
}
