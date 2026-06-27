namespace Common
{
    public interface AttackPipeline
    {
        void Caluclate(IHittable targer, IDamagable attacker);
    }
}
