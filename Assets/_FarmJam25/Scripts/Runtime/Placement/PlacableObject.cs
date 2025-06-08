using UnityEngine;

namespace NJG.Runtime.Placement
{
    public abstract class PlacableObject : MonoBehaviour, IPlacable
    {
        public abstract Vector2Int[] occupied_cells { get; }
    }
}