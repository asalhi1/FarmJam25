using UnityEngine;

public interface IPlacable
{
    public Vector2Int[] occupied_cells { get; }
}