public class CyclingActivity : Activity
{
    private double _distanceInKm; // Distance in kilometers

    public CyclingActivity(string date, int durationInMinutes, double distanceInKm)
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
        return 60 / GetSpeed(); // Pace = 60 / speed (in km/h)
    }

    public override string GetActivityTitle()
    {
        return "Cycling";
    }
}
