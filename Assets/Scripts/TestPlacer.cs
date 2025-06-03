using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlacementEntry
{
    public GameObject placeable_obj;
    public Vector2Int position;
}

public class TestPlacer : MonoBehaviour
{
    public GridManager grid_manager;

    public List<PlacementEntry> placements;

    void Start()
    {
        foreach (PlacementEntry entry in placements)
        {
            GameObject instance = Instantiate(entry.placeable_obj);
            IPlacable placable = instance.GetComponent<IPlacable>();

            if (placable == null)
            {
                Debug.Log($"Missing IPlacable component on {entry.placeable_obj.name}");
                Destroy(instance);
                continue;
            }

            if (grid_manager.canPlaceAt(entry.position, placable))
            {
                Vector3 world_pos = grid_manager.flatToWorld(entry.position);
                instance.transform.position = world_pos;
                grid_manager.placeObject(entry.position, placable);
                Debug.Log("Placed object at " + entry.position);
            }
            else
            {
                Debug.Log($"Could not place object at {entry.position}, space is occupied.");
                Destroy(instance);
            }
        }
    }
}