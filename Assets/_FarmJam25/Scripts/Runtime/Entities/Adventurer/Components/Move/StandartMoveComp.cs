using UnityEngine;
using UnityEngine.AI;

namespace NJG.Runtime.Entities.Adventurer.Components
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class StandartMoveComp : MonoBehaviour, IMoveComp
    {
        private NavMeshAgent _navMeshAgent;

        private void OnEnable()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public bool TryMoveTo(Vector3 destination)
        {
            _navMeshAgent.isStopped = false;
            return _navMeshAgent.SetDestination(destination);
        }

        public void StopMovement(bool shouldInstantStop = false)
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.ResetPath();
            
            if(shouldInstantStop)
                _navMeshAgent.velocity = Vector3.zero;
        }
    }
}