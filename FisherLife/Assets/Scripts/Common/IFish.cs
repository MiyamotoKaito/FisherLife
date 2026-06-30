namespace Commons
{
    public interface IFish : IHittable
    {
        string Name { get; }
        int MoneyAmount { get; }
        int Level { get; }
    }
}
