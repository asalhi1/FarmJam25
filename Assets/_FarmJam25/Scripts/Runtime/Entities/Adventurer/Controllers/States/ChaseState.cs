using UnityEngine;

namespace NJG.Runtime.Entities.Adventurer.States
{
    [CreateAssetMenu(fileName = "SO_State_Chase", menuName = "NJG/States/Chase")]
    public class ChaseState : AdventurerState
    {
        public override float GetStatePriority()
        {
            if (_controller.ChaseTarget != null)
                return 5;
            else 
                return 0;
        }

        public override void OnLogicUpdate()
        {
            if (_controller.ChaseTarget.Transform)
            {
                if(!_controller.CMove.TryMoveTo(_controller.ChaseTarget.Transform.position))
                    _controller.CalculateState();
            }
            else
                _controller.CalculateState();
        }
    }
}