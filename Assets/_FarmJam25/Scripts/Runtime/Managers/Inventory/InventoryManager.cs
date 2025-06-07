using System;
using System.Collections.Generic;
using UnityEngine;

namespace NJG.Runtime.Managers
{
    public class InventoryManager 
    {
        private Dictionary<EResource, int> _resources;
        
        public int GetResourceAmount(EResource type)
        {
            if (type == EResource.None)
            {
                Debug.LogError($"Resource Type None is an invalid type for PlayerState GetResource.");
                return -1;
            }

            return _resources.GetValueOrDefault(type, 0);
        }

        public bool HasEnoughResource(EResource type, int amount)
        {
            if (type == EResource.None)
            {
                Debug.LogError($"Resource Type None is an invalid type for PlayerState GetResource.");
                return false;
            }

            return GetResourceAmount(type) >= amount;
        }

        // Modifies resources with error checking. Returns remaining amount of resource.
        public int ModifyResource(EResource type, int delta)
        {
            if (type == EResource.None)
            {
                Debug.LogError($"Attempted Resource None to modify by {delta}.");
                return -1;
            }

            int resourceAmount = _resources.GetValueOrDefault(type, 0);
            if (resourceAmount + delta < 0)
            {
                Debug.LogWarning(
                    $"Attempted to modify Resource {Enum.GetName(typeof(EResource), type)} by {delta} to {resourceAmount - delta} (less than zero). Ignoring.");
                return resourceAmount;
            }

            _resources[type] = resourceAmount + delta;
            return _resources[type];
        }

        // Assumes the costs to be positive (1 vitality, 3 detritus)
        public bool QueryPurchase(Dictionary<EResource, int> purchase, bool purchaseIfAble)
        {
            foreach (var resourceCost in purchase)
            {
                // if the player has less of a resource than they need for the purchase
                if (GetResourceAmount(resourceCost.Key) < resourceCost.Value)
                {
                    return false;
                }
            }

            if (purchaseIfAble) // only spends the resources, making the structure is handled by the caller
            {
                foreach (var resourceCost in purchase)
                {
                    ModifyResource(resourceCost.Key, -resourceCost.Value); //note the negative.
                }
            }

            return true;
        }

        public void SetResource(EResource type, int amount)
        {
            if (type == EResource.None)
            {
                Debug.LogError($"Attempted to set Resource Type None to {amount}. Type None cannot be set");
                return;
            }

            if (amount < 0)
            {
                Debug.LogWarning(
                    $"Attempted to set Resource Type #{(int)type} to {amount}. Below 0, setting to 0 instead.");
                _resources[type] = 0;
                return;
            }

            _resources[type] = amount;
        }
    }
}
