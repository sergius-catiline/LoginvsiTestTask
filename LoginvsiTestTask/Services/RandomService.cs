namespace LoginvsiTestTask.Services;

/// <summary>
/// <see cref="IRandomService"/>
/// </summary>
public class RandomService : IRandomService
{
    private readonly Random _random = new Random();
    public int Next(int max)
    {
        return _random.Next(max);
    }
}