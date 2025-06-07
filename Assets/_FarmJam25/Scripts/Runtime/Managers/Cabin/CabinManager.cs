using System;
using NJG.Runtime.Signals;
using UnityEngine;
using Zenject;

namespace NJG.Runtime.Managers
{
    public class CabinManager 
    {
        private float _cabinHealth; 
        private float _cabinMaxHealth = 10;
        private bool _isCabinAlive;
        
        private SignalBus _signalBus;

        public bool IsCabinAlive => _isCabinAlive;
        
        public CabinManager()
        {
            _cabinHealth = _cabinMaxHealth;
            _isCabinAlive = true;
        }

        [Inject]
        void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public float GetCabinMaxHealth() { return _cabinMaxHealth; } 


        public void ModifyCabinMaxHealth(float deltaMaxHealth)
        {
            SetCabinMaxHealth(_cabinMaxHealth + deltaMaxHealth);
        }
        
        public void SetCabinMaxHealth(float newMaxHealth)
        {
            _cabinMaxHealth = Math.Max(0, newMaxHealth);
            _cabinHealth = Math.Clamp(_cabinHealth, 0, _cabinMaxHealth);
        }
        
        public float GetCabinHealth() { return _cabinHealth; }

        public void ModifyCabinHealth(float deltaHealth)
        {
            if(!_isCabinAlive)
                return;
            
            _cabinHealth = Math.Clamp(_cabinHealth + deltaHealth, 0, _cabinMaxHealth);
            _signalBus.Fire(new CabinHealthChangedSignal(_cabinHealth, _cabinMaxHealth, _cabinHealth/_cabinMaxHealth));
            
            if(_cabinHealth == 0)
                CabinDied();
        }

        private void CabinDied()
        {
            _isCabinAlive = false;
            _signalBus.Fire(new CabinDiedSignal());
        }
        
    }
}