namespace Common
{
    public interface IRod
    {
        int Power { get; }

        float CriticalMultiplier { get; }
        float CriticalProbability { get; }
    }
}