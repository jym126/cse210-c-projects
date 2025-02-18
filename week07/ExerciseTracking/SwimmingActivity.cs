public class SwimmingActivity : Activity
{
    private int _laps; // Number of laps swum

    public SwimmingActivity(string date, int durationInMinutes, int laps)
        : base(date, durationInMinutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        // Swimming distance in km (assuming 1 lap = 50 meters)
        return (_laps * 50.0) / 1000; // Convert meters to kilometers
    }

    public override double GetSpeed()
    {
        // Speed in km/h
        return (GetDistance() / DurationInMinutes) * 60;
    }

    public override double GetPace()
    {
        // Pace in min/km
        return DurationInMinutes / GetDistance();
    }

    public override string GetActivityTitle()
    {
        return "Swimming";
    }
}
