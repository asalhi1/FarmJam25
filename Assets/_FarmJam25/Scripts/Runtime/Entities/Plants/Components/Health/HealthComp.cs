using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _FarmJam25.Scripts.Runtime.Entities.Plants.Components
{
    public class HealthComp : MonoBehaviour
    {
        [field: FoldoutGroup("Settings"), SerializeField]
        public float MaxHealth{get; private set;} = 10;
        [field: FoldoutGroup("Settings"), SerializeField, ReadOnly]
        public float CurrentHealth{get; private set;} = 10;

        public bool IsAlive { get; private set; } = true;
        
        public event Action OnHealthChanged;
        public event Action OnDeath;

        private void OnEnable()
        {
            CurrentHealth = MaxHealth;
        }

        public void DecreaseHealth(float amount)
        {
            if(!IsAlive)
                return;
            if(amount < 0)
                Debug.LogError("decrease amount is negative");
            
            CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, MaxHealth);
            OnHealthChanged?.Invoke();
            TryDeath();
        }
        
        public void IncreaseHealth(float amount)
        {
            if(!IsAlive)
                return;
            if(amount < 0)
                Debug.LogError("increase amount is negative");
            
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
            OnHealthChanged?.Invoke();
        }

        private void TryDeath()
        {
            if(!IsAlive || CurrentHealth != 0)
                return;
            
            IsAlive = false;
            OnDeath?.Invoke();

            Destroy(gameObject);
        }
    }
}