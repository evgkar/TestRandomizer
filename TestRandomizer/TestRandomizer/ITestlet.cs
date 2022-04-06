namespace TestRandomizer;

public interface ITestlet
{
    string TestletId { get; }
    List<Item> Randomize();
}
