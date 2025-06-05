using UnityEngine;

namespace NJG.Runtime.Entities.Adventurer
{
    public class AdventurerState : ScriptableObject
    {
        protected AdventurerController _controller;

        public bool CanBeExited { get; private set; } = true;

        public virtual void Initialize(AdventurerController controller)
        {
            _controller = controller;
        }
        
        public virtual void OnStateEnter() { }
        
        public virtual void OnLogicUpdate() { }

        public virtual float GetStatePriority()  
        {
            return 1;
        }
        
        public virtual void OnStateExit() { }
        
    }
}