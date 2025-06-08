    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using MEC;
    using Sirenix.OdinInspector;

    namespace NJG.Runtime.Entities.Plants.Components
    {
        public class MeleAttackComp : MonoBehaviour
        {
            [FoldoutGroup("Settings"), SerializeField]
            private float _damage = 3;
            [FoldoutGroup("Settings"), SerializeField]
            private float _range = 2.5f;
            [FoldoutGroup("Settings"), SerializeField]
            private LayerMask _layerMask;
            [FoldoutGroup("Settings"), SerializeField]
            private Transform _attackPoint;
            
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
            
            private void OnDestroy()
            {
                if (_attackHandle.IsRunning)
                    Timing.KillCoroutines(_attackHandle);
            }

            private void FixedUpdate()
            {
                if(!IsInCooldown())
                    if(TryFindDamagable(out IDamagable damagable))
                        Attack(damagable);
            }
            
            private void Attack(IDamagable damagable)
            {
                if(IsInCooldown())
                    return;

                _attackHandle = Timing.RunCoroutine(AttackRoutine(damagable), Segment.Update, gameObject);
            }

            private void CancellAttack()
            {
                if(!_attackHandle.IsRunning)
                    return;
                Timing.KillCoroutines(_attackHandle);
                _isAttacking = false;
                _cooldownTimer = _cooldown;
            }
            
            private bool IsInCooldown()
            {
                return _isAttacking || _cooldownTimer > 0;
            }
            
            private IEnumerator<float> AttackRoutine(IDamagable damagable)
            {
                _isAttacking = true;
                Transform attackTransform = damagable.Transform;

                yield return Timing.WaitForSeconds(_attackTime);
                
                if(attackTransform )
                    damagable.Damage(_damage, transform.forward, null);
                

                yield return Timing.WaitForSeconds(_recoveryTime);

                _cooldownTimer = _cooldown;
                _isAttacking = false;
            }

            private bool TryFindDamagable(out IDamagable damagable)
            {
                damagable = null;
                Collider[] colliders = Physics.OverlapSphere(_attackPoint.position, _range, _layerMask);

                foreach (var VARIABLE in colliders)
                    if (VARIABLE.TryGetComponent(out IDamagable damagableComponent))
                    {
                        damagable = damagableComponent;
                        return true;
                    }
                
                return false;
            }
            
        }
    }