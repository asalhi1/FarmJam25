using UnityEngine;

namespace NGJ.Runtime.Placement
{
    public interface IPlacable
    {
        [SerializeField] public Vector2Int[] occupied_cells { get; }
    }
}