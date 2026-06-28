namespace Commons
{
    public interface AttackPipeline
    {
        void Caluclate(IHittable targer, IDamagable attacker);
    }
}
