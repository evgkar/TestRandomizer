namespace TestRandomizer;

/// <summary>
/// Represents a set of test items of 2 categories: Operational and Pretest
/// Creates a randomly shuffled set of test items based on the initial set.
/// </summary>
public interface ITestlet
{
    /// <summary>
    /// An identifier of the Testlet.
    /// </summary>
    string TestletId { get; }

    /// <summary>
    /// Randomizes list of Items in the following way:
    /// Shuffles a list of items,
    /// then places 2 Pretest items at the first positions.
    /// </summary>
    /// <returns></returns>
    List<Item> Randomize();
}
