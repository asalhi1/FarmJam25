using UnityEngine;

namespace NJG.Runtime
{
    public interface IDamagable
    {
        public Transform Transform { get; }
        
        public void Damage(float damage, Vector3 DamageDirection, IDamageGiver damageGiver = null);
    }
}