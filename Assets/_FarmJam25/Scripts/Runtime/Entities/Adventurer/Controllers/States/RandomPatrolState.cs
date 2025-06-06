using UnityEngine;
using UnityEngine.AI;

namespace NJG.Runtime.Entities.Adventurer.States
{
    [CreateAssetMenu(fileName = "SO_State_RandomPatrol", menuName = "NJG/States/RandomPatrol")]
    public class RandomPatrolState : AdventurerState
    {
        [SerializeField] private float _changeDestinationTime = 5f;

        [SerializeField] private float _patrolRadius = 10f;

        private float _timer;

        public override void OnStateEnter()
        {
            ChangeDestination();
        }

        public override void OnLogicUpdate()
        {
            _timer += Time.deltaTime;
            if(_timer >= _changeDestinationTime)
                ChangeDestination();
        }

        private void ChangeDestination()
        {
            _timer = 0;
            Vector3 randomPoint = GetRandomNavMeshLocation(_controller.transform.position, _patrolRadius);
            _controller.CMove.TryMoveTo(randomPoint);
        }

        private Vector3 GetRandomNavMeshLocation(Vector3 center, float radius)
        {
            for (int i = 0; i < 30; i++) 
            {
                Vector3 randomPos = center + Random.insideUnitSphere * radius;
                randomPos.y = center.y; 

                if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                    return hit.position;
            }

            return center; 
        }
    }
}