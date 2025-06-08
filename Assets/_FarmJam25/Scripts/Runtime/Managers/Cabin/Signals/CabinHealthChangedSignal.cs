namespace NJG.Runtime.Signals
{
    public struct CabinHealthChangedSignal
    {
        public float Health;
        public float MaxHealth;
        public float HealthRatio;

        public CabinHealthChangedSignal(float health, float maxHealth, float healthRatio)
        {
            Health = health;
            MaxHealth = maxHealth;
            HealthRatio = healthRatio;
        }
    }
}