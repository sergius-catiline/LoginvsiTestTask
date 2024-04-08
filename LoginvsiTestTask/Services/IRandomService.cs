namespace LoginvsiTestTask.Services;

/// <summary>
/// Wrapper on Random class to use in DI and simplify unit testing 
/// </summary>
public interface IRandomService
{
    /// <summary>
    ///  Returns a non-negative random integer that is less than the specified maximum.
    /// </summary>
    /// <param name="max">Maximum value, i.e. it exclude bound</param>
    /// <returns>Random number</returns>
    int Next(int max);
}