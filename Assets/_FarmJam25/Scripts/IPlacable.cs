using UnityEngine;

namespace _FarmJam25.Scripts
{
    public interface IPlacable
    {
        [SerializeField] public Vector2Int[] occupied_cells { get; }
    }
}