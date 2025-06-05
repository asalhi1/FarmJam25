using UnityEngine;

namespace NJG.Runtime.Entities.Adventurer.States
{
    [CreateAssetMenu(fileName = "SO_State_Attack", menuName = "NJG/States/Attack")]
    public class AttackState : AdventurerState
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            CanBeExited = true;
            TryAttack();
        }

        public override void OnLogicUpdate()
        {
            TryAttack();
        }

        public override float GetStatePriority()
        {
            if (_controller.AttackTarget != null)
                return 10;
            else
                return 0;
        }

        private void TryAttack()
        {
            if(!_controller.CAttack.IsInCooldown())
                StartAttack();
        }

        private void StartAttack()
        {
            CanBeExited = false;
            _controller.CAttack.Attack(_controller.AttackTarget, OnAttackOver);
        }

        private void OnAttackOver()
        {
            CanBeExited = true;
            _controller.CalculateState();
        }
    }
}