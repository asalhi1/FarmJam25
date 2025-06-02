using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int cell_size = 1;
    private Dictionary<Vector2Int, IPlacable> gridData = new();

    public bool canPlaceAt(Vector2Int origin, IPlacable obj)
    {
        foreach (var cell in obj.occupied_cells)
        {
            Vector2Int world_cell = origin + cell;
            if (gridData.ContainsKey(world_cell)) return false;
        }

        return true;
    }

    public void placeObject(Vector2Int origin, IPlacable obj)
    {
        foreach (var cell in obj.occupied_cells)
        {
            gridData[origin + cell] = obj;
        }
    }

    public Vector3 flatToWorld(Vector2Int flat_pos)
    {
        return new Vector3(flat_pos.x * cell_size, 0f, flat_pos.y * cell_size);
    }
}
