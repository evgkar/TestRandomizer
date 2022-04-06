namespace TestRandomizer;

internal class Randomizer : IRandomizer
{
    private readonly Random _random;

    public Randomizer()
    {
        _random = new Random();
    }

    public int GetRandomValue()
        => _random.Next();
}
