using UnityEngine;
using Sirenix.OdinInspector;
using Zenject;

namespace NJG.Runtime.Entities
{
    public class Cabin : MonoBehaviour, IDamagable
    {
        [FoldoutGroup("Health"), SerializeField] 
        private float _health = 10;
        
        public Transform Transform => transform;
        
        public void Damage(float damage, Vector3 DamageDirection, IDamageGiver damageGiver = null)
        {
            if(gameObject == null)
                return;
            
            _health -= damage;
            if(_health <= 0)
                Destroy(gameObject);
        }
    }
}