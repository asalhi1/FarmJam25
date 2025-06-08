using NJG.Runtime.Managers;
using UnityEngine;
using Sirenix.OdinInspector;
using Zenject;

namespace NJG.Runtime.Entities
{
    public class Cabin : MonoBehaviour, IDamagable
    {
        CabinManager _cabinManager;
        public Transform Transform => transform;

        [Inject]
        void Construct(CabinManager cabinManager)
        {
            _cabinManager = cabinManager;
        }
        
        public void Damage(float damage, Vector3 DamageDirection, IDamageGiver damageGiver = null)
        {
            _cabinManager.ModifyCabinHealth(-damage);
            
            if(!_cabinManager.IsCabinAlive)
                Destroy(gameObject);
        }
    }
}