using UnityEngine;

public class TestHouse : MonoBehaviour, IPlacable
{
    public Vector2Int[] occupied_cells => new Vector2Int[]
    {
        new Vector2Int(0, 0)
    };
}
