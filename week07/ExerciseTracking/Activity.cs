using System;

public abstract class Activity
{
    // Private member variables
    private string _date;
    private int _durationInMinutes;

    // Constructor
    public Activity(string date, int durationInMinutes)
    {
        _date = date;
        _durationInMinutes = durationInMinutes;
    }

    // Encapsulated properties
    public string Date => _date;
    public int DurationInMinutes => _durationInMinutes;

    // Abstract methods that must be overridden in derived classes
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();
    public abstract string GetActivityTitle();

    // Method to generate a summary for all activities
    public virtual string GetSummary()
    {
        return $"{GetActivityTitle()} Activity\n" +
               $"Date: {Date}\n" +
               $"Duration: {DurationInMinutes} minutes\n" +
               $"Distance: {GetDistance():0.0} km\n" +
               $"Speed: {GetSpeed():0.0} km/h\n" +
               $"Pace: {GetPace():0.0} min/km";
    }
}
