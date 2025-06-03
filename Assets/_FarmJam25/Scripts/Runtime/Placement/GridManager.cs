using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int cell_size = 1;
    private Dictionary<Vector2Int, IPlacable> grid_data = new();

    public bool canPlaceAt(Vector2Int origin, IPlacable obj)
    {
        foreach (var cell in obj.occupied_cells)
        {
            Vector2Int world_cell = origin + cell;
            if (grid_data.ContainsKey(world_cell)) return false;
        }

        return true;
    }

    public void placeObject(Vector2Int origin, IPlacable obj)
    {
        foreach (var cell in obj.occupied_cells)
        {
            grid_data[origin + cell] = obj;
        }
    }

    public void removeObject(Vector2Int origin)
    {
        if (!grid_data.TryGetValue(origin, out IPlacable obj))
        {
            Debug.Log($"Can't remove object at {origin}");
            return;
        }

        foreach (var cell in obj.occupied_cells)
        {
            grid_data.Remove(origin + cell);
        }

        MonoBehaviour mb = obj as MonoBehaviour;
        Destroy(mb.gameObject);
    }

    public Vector3 flatToWorld(Vector2Int flat_pos)
    {
        return new Vector3(flat_pos.x * cell_size, 0f, flat_pos.y * cell_size);
    }
}
