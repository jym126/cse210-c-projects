public class RunningActivity : Activity
{
    private double _distanceInKm; // Distance in kilometers

    public RunningActivity(string date, int durationInMinutes, double distanceInKm)
        : base(date, durationInMinutes)
    {
        _distanceInKm = distanceInKm;
    }

    public override double GetDistance()
    {
        return _distanceInKm;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / DurationInMinutes) * 60; // Speed = distance / time * 60
    }

    public override double GetPace()
    {
        return DurationInMinutes / GetDistance(); // Pace = time / distance
    }

    public override string GetActivityTitle()
    {
        return "Running";
    }
}
