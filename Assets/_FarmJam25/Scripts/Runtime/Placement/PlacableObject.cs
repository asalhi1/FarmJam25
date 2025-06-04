using UnityEngine;

namespace NGJ.Runtime.Placement
{
    public abstract class PlacableObject : MonoBehaviour, IPlacable
    {
        public abstract Vector2Int[] occupied_cells { get; }
    }
}