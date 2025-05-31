using UnityEngine;

public class TestLargeHouse : MonoBehaviour, IPlacable
{
    public Vector2Int[] occupied_cells => new Vector2Int[]
    {
        new Vector2Int(0, 0),
        new Vector2Int(0, 1)
    };
}
