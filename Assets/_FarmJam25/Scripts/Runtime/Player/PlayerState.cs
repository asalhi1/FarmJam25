using System;
using System.Collections.Generic;
using UnityEngine;

namespace NJG.Runtime.Player
{
    // Get, Modify and Set functions for Cabin Health, Cabin Max Health, and each of the Resources
    public class PlayerState
    { 
        
        private Dictionary<Vector2Int, Placement.IPlacable> _gridInformation;

        public PlayerState()
        {
            
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