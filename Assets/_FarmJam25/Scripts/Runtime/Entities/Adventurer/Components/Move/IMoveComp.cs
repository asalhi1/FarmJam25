using UnityEngine;

namespace NJG.Runtime.Entities.Adventurer.Components
{
    public interface IMoveComp
    {
        public bool TryMoveTo(Vector3 destination);

        public void StopMovement(bool shouldInstantStop = false);
    }
}
