using UnityEngine;

namespace NJG.Runtime.Entities.Adventurer.States
{
    [CreateAssetMenu(fileName = "SO_State_GoCabin", menuName = "NJG/States/GoCabin")]
    public class GoCabinState : AdventurerState
    {
        public override void OnStateEnter()
        {
            if(!_controller.CMove.TryMoveTo(Vector3.zero))
                Debug.Log("GoCabinState : TryMoveTo returned false");
        }

        public override void OnStateExit()
        {
            _controller.CMove.StopMovement();
        }
    }
}