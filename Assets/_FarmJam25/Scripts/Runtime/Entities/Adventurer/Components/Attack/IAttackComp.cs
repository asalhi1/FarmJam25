using System;
using UnityEngine;

namespace NJG.Runtime.Entities.Adventurer.Components
{
    public interface IAttackComp 
    {
        public void Attack(IDamagable damagable, Action attackOverCallback);

        public void CancellAttack();

        public bool IsInCooldown();
    }
}