using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NJG.Runtime.Entities.Adventurer.Components
{
    public class TargetSelector : MonoBehaviour
    {
        [FoldoutGroup("Settings"), SerializeField]
        private LayerMask _layerMask;

        [FoldoutGroup("Settings"), SerializeField]
        private float _attackRange;
        
        [FoldoutGroup("Settings"), SerializeField]
        private float _chaseRange;

        public STargets GetTargets()
        {
            STargets targets = new STargets();

            IDamagable[] damagables = GetDamagablesInRadius(transform.position, _attackRange);

            if (damagables.Length > 0)
            {
                targets.AttackTarget = damagables[0];
                targets.ChaseTarget = damagables[0];
            }
            else
            {
                damagables = GetDamagablesInRadius(transform.position, _chaseRange);
                if(damagables.Length > 0)
                    targets.ChaseTarget = damagables[0];
            }
            return targets;
        }
        

        private IDamagable[] GetDamagablesInRadius(Vector3 centerPos, float radius)
        {
            return Physics.OverlapSphere(centerPos, radius, _layerMask)
                .Select(c => c.GetComponent<IDamagable>())
                .Where(d => d != null)
                .ToArray();
        }
    }

    public struct STargets
    {
        public IDamagable AttackTarget;
        
        public IDamagable ChaseTarget;
    }
}