using System;
using System.Collections.Generic;
using UnityEngine;

namespace NGJ.Runtime.Player
{
    // Get, Modify and Set functions for Cabin Health, Cabin Max Health, and each of the Resources
    public class PlayerState
    { 
        private int _cabinHealth; // cabin health does not use the resource system because it has an upper bounds: maxHealth
        private int _cabinMaxHealth;
        private Dictionary<EResource, int> _resource;
        private Dictionary<Vector2Int, Placement.IPlacable> _gridInformation;

        public PlayerState()
        {
            _cabinHealth = _cabinMaxHealth;
            
        }
        /* ---- CABIN ---- */

        public int GetCabinMaxHealth() { return _cabinMaxHealth; } // using this over a property because it's more flexible and more intuitive

        public void ModifyCabinMaxHealth(int deltaMaxHealth)
        {
            SetCabinMaxHealth(_cabinMaxHealth + deltaMaxHealth);
        }
        
        public void SetCabinMaxHealth(int newMaxHealth)
        {
            _cabinMaxHealth = Math.Max(0, newMaxHealth);
            _cabinHealth = Math.Clamp(_cabinHealth, 0, _cabinMaxHealth);
        }
        
        public int GetCabinHealth() { return _cabinHealth; }

        // Used to modify cabin's health (positive is healing, negative is damage). If raised above the max health, the max health will automatically adjust
        public void ModifyCabinHealth(int deltaHealth)
        {
            SetCabinHealth(_cabinHealth + deltaHealth);
        }

        // Used to set cabin's health directly. If raised above the max health, the max health will automatically adjust
        public void SetCabinHealth(int newCabinHealth)
        {
            _cabinHealth = Math.Max(newCabinHealth, 0);
            if (_cabinHealth > _cabinMaxHealth)
            {
                _cabinMaxHealth = _cabinHealth;
            }
        }
        
        /* ---- RESOURCES ---- */
        
        public int GetResource(EResource type)
        {
            if (type == EResource.None)
            {
                Debug.LogError($"Resource Type None is an invalid type for PlayerState GetResource.");
                return -1;
            }
            return _resource.GetValueOrDefault(type, 0);
        }

        // Modifies resources with error checking. Returns remaining amount of resource.
        public int ModifyResource(EResource type, int delta)
        {
            if (type == EResource.None)
            {
                Debug.LogError($"Attempted Resource None to modify by {delta}.");
                return -1;
            }
            int resourceAmount = _resource.GetValueOrDefault(type, 0);
            if (resourceAmount + delta < 0)
            {
                Debug.LogWarning($"Attempted to modify Resource {Enum.GetName(typeof(EResource), type)} by {delta} to {resourceAmount - delta} (less than zero). Ignoring.");
                return resourceAmount;
            }

            _resource[type] = resourceAmount + delta;
            return _resource[type];
        }

        // Assumes the costs to be positive (1 vitality, 3 detritus)
        public bool QueryPurchase(Dictionary<EResource, int> purchase, bool purchaseIfAble)
        {
            foreach (var resourceCost in purchase)
            {
                // if the player has less of a resource than they need for the purchase
                if (GetResource(resourceCost.Key) < resourceCost.Value)
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
                Debug.LogWarning($"Attempted to set Resource Type #{(int)type} to {amount}. Below 0, setting to 0 instead.");
                _resource[type] = 0;
                return;
            }

            _resource[type] = amount;
        }

        /* ---- GRID INFO ---- */

        public void SetGridInformation(Dictionary<Vector2Int, Placement.IPlacable> grid)
        {
            _gridInformation = grid;
        }

        public Dictionary<Vector2Int, Placement.IPlacable> GetGridInformation()
        {
            return _gridInformation;
        }
        
        
    }
}