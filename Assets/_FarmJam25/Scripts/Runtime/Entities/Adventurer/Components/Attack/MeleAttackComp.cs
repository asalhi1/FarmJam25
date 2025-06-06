    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using MEC;
    using Sirenix.OdinInspector;

    namespace NJG.Runtime.Entities.Adventurer.Components
    {
        public class MeleAttackComp : MonoBehaviour, IAttackComp
        {
            [FoldoutGroup("Settings"), SerializeField]
            private float _damage = 3;
            
            [FoldoutGroup("Times"), SerializeField]
            private float _cooldown;
            [FoldoutGroup("Times"), SerializeField]
            private float _attackTime;
            [FoldoutGroup("Times"), SerializeField]
            private float _recoveryTime;
            
            private float _cooldownTimer;
            private bool _isAttacking;
            private CoroutineHandle _attackHandle;
            
            private void Update()
            {
                _cooldownTimer -= Time.deltaTime;
            }
            
            public void Attack(IDamagable damagable, Action attackOverCallback)
            {
                if(IsInCooldown())
                    return;

                _attackHandle = Timing.RunCoroutine(AttackRoutine(damagable, attackOverCallback), Segment.Update, gameObject);
            }

            public void CancellAttack()
            {
                if(!_attackHandle.IsRunning)
                    return;
                Timing.KillCoroutines(_attackHandle);
                _isAttacking = false;
                _cooldownTimer = _cooldown;
            }
            
            public bool IsInCooldown()
            {
                return _isAttacking || _cooldownTimer > 0;
            }
            
            private IEnumerator<float> AttackRoutine(IDamagable damagable, Action attackOverCallback)
            {
                _isAttacking = true;

                yield return Timing.WaitForSeconds(_attackTime);
                
                if(damagable != null && damagable.Transform)
                    damagable.Damage(_damage, transform.forward, null);

                yield return Timing.WaitForSeconds(_recoveryTime);

                _cooldownTimer = _cooldown;
                _isAttacking = false;
                attackOverCallback();
            }

            
        }
    }