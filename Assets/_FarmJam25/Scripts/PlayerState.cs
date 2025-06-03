using System;
using System.Collections.Generic;
using NJG.Utilities.EventBus;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;


namespace _FarmJam25.Scripts
{
    public enum ResourceType
    {
        None = 0,
        Detritus = 1,
        Supports = 2,
        Vitality = 3
    }
    
    // Get, Modify and Set functions for Cabin Health, Cabin Max Health, and each of the ResourceTypes
    public class PlayerState : MonoBehaviour
    { 
        [SerializeField]
        private int cabinHealth; // cabin health does not use the resource system because it has an upper bounds: maxHealth
        [SerializeField]
        private int cabinMaxHealth;
        [SerializeField]
        private Dictionary<ResourceType, int> _resource;
        [SerializeField]
        private Dictionary<Vector2Int, IPlacable> _gridInformation;

        PlayerState()
        {
            cabinHealth = cabinMaxHealth;
            
        }
        /* ---- CABIN ---- */

        public int GetCabinMaxHealth() { return cabinMaxHealth; } // using this over a property because it's more flexible and more intuitive

        public void ModifyCabinMaxHealth(int deltaMaxHealth)
        {
            SetCabinMaxHealth(cabinMaxHealth + deltaMaxHealth);
        }
        
        public void SetCabinMaxHealth(int newMaxHealth)
        {
            cabinMaxHealth = Math.Max(0, newMaxHealth);
            cabinHealth = Math.Clamp(cabinHealth, 0, cabinMaxHealth);
        }
        
        public int GetCabinHealth() { return cabinHealth; }

        // Used to modify cabin's health (positive is healing, negative is damage). If raised above the max health, the max health will automatically adjust
        public void ModifyCabinHealth(int deltaHealth)
        {
            SetCabinHealth(cabinHealth + deltaHealth);
        }

        // Used to set cabin's health directly. If raised above the max health, the max health will automatically adjust
        public void SetCabinHealth(int newCabinHealth)
        {
            cabinHealth = Math.Max(newCabinHealth, 0);
            if (cabinHealth > cabinMaxHealth)
            {
                cabinMaxHealth = cabinHealth;
            }
        }
        
        /* ---- RESOURCES ---- */
        
        public int GetResource(ResourceType type)
        {
            if (type == ResourceType.None)
            {
                Debug.LogError($"Resource Type Null is an invalid type for PlayerState GetResource.");
                return -1;
            }
            return _resource.GetValueOrDefault(type, 0);
        }

        // Modifies resources with error checking. Returns remaining amount of resource.
        public int ModifyResource(ResourceType type, int delta)
        {
            if (type == ResourceType.None)
            {
                Debug.LogError($"Attempted Resource Type Null to modify by {delta}.");
                return -1;
            }
            int resourceAmount = _resource.GetValueOrDefault(type, 0);
            if (resourceAmount + delta < 0)
            {
                Debug.LogWarning($"Attempted to modify Resource Type {Enum.GetName(typeof(ResourceType), type)} by {delta} to {resourceAmount - delta} (less than zero). Ignoring.");
                return resourceAmount;
            }

            _resource[type] = resourceAmount + delta;
            return _resource[type];
        }

        // Assumes the costs to be positive (1 vitality, 3 detritus)
        public bool QueryPurchase(Dictionary<ResourceType, int> purchase, bool purchaseIfAble)
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
        
        public void SetResource(ResourceType type, int amount)
        {
            if (type == ResourceType.None)
            {
                Debug.LogError($"Attempted to set Resource Type Null to {amount}. Type Null cannot be set");
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

        void SetGridInformation(Dictionary<Vector2Int, IPlacable> grid)
        {
            _gridInformation = grid;
        }

        Dictionary<Vector2Int, IPlacable> GetGridInformation()
        {
            return _gridInformation;
        }
        
        
    }
}