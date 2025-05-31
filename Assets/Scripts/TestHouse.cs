using UnityEngine;

public class TestHouse : MonoBehaviour, IPlacable
{
    public Vector2Int[] occupied_cells => new Vector2Int[]
    {
        new Vector2Int(0, 0)
    };

    public Vector2 pivot_offset => new Vector2(0, -.5f);
}
