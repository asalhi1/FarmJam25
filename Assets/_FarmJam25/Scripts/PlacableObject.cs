using UnityEngine;

namespace _FarmJam25.Scripts
{
    public abstract class PlacableObject : MonoBehaviour, IPlacable
    {
        public abstract Vector2Int[] occupied_cells { get; }
    }
}