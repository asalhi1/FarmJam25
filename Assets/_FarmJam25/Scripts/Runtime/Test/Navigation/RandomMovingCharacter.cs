using UnityEngine;
using UnityEngine.AI;

namespace NJG.Runtime.Test.Navigation
{
    public class RandomMovingCharacter : MonoBehaviour
    {
        [SerializeField] private float _maxRadiusForRandomPoint = 10;

        private NavMeshAgent _agent;

        private void OnEnable()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void GoToARandomLocation()
        {
            if (NavMesh.SamplePosition(GetRandomPosition(), out NavMeshHit hit, _maxRadiusForRandomPoint, NavMesh.AllAreas))
                _agent.SetDestination(hit.position);
        }
        
        public void TeleportToRandomLocation()
        {
            transform.position = GetRandomPosition();
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 randomDirection = Random.insideUnitSphere * _maxRadiusForRandomPoint;
            randomDirection += transform.position;
            randomDirection.y = 0;
            return randomDirection;
        }
    }
}