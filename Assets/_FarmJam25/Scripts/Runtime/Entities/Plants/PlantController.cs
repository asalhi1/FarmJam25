using System;
using _FarmJam25.Scripts.Runtime.Entities.Plants.Components;
using UnityEngine;

namespace NJG.Runtime.Entities.Plants
{
    public class PlantController : MonoBehaviour, IDamagable
    {
        public HealthComp CHealth { get; private set; }
        
        public Transform Transform => transform;

        private void OnEnable()
        {
            CHealth = GetComponent<HealthComp>();
        }


        public void Damage(float damage, Vector3 DamageDirection, IDamageGiver damageGiver = null)
        {
            CHealth.DecreaseHealth(damage);
        }
    }
}