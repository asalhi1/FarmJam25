using UnityEngine;

public abstract class PlacableObject : MonoBehaviour, IPlacable
{
    public abstract Vector2Int[] occupied_cells { get; }
    public abstract Vector2 pivot_offset { get; }
}
