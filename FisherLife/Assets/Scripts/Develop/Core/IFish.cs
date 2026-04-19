namespace Miyamoto.FisherLife.Develop.Core
{
    public interface IFish
    {
        void Patrol();
        void FindBait();
        void CaughtBait();
    }
}
