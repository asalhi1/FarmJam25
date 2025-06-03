using System.Collections.Generic;
using UnityEngine;

namespace NJG.Runtime.Placement
{
    public class GridManager : MonoBehaviour
    {
        public int cell_size = 1;
        
        private Dictionary<Vector2Int, IGridObject> grid_data = new();
        
        public bool CanPlaceAt(Vector2Int desiredCellIndex, IGridObject obj)
        {
            foreach (var cell in obj.CellPositions)
            {
                Vector2Int world_cell = desiredCellIndex + cell;
                if (grid_data.ContainsKey(world_cell)) 
                    return false;
            }
            return true;
        }

        public bool TryPlaceObject(Vector2Int desiredCellIndex, IGridObject obj)
        {
            if(!CanPlaceAt(desiredCellIndex, obj))
                return false;
            
            foreach (var cell in obj.CellPositions)
                grid_data[desiredCellIndex + cell] = obj;

            obj.PlaceOnMap(desiredCellIndex, GetCellPosition(desiredCellIndex));
            
            return true;
        }

        public bool IsAtPlace(IGridObject obj)
        {
            if(!grid_data.ContainsKey(obj.GridIndex + obj.CellPositions[0]))
                return false;
            return grid_data[obj.GridIndex + obj.CellPositions[0]] == obj;
        }

        public void RemoveObject(IGridObject obj)
        {
            if (!IsAtPlace(obj))
            {
                Debug.LogWarning("Object is not in grid");
                return;
            }

            foreach (var cell in obj.CellPositions)
                grid_data.Remove(obj.GridIndex + cell);
        }
        
        public Vector3 GetCellPosition(Vector2Int cellIndex)
        {
            return new Vector3(cellIndex.x * cell_size, 0f, cellIndex.y * cell_size);
        }

        public Vector2Int GetCellIndexFromPosition(Vector3 worldPosition)
        {
            int x = Mathf.FloorToInt((worldPosition.x + cell_size * 0.5f) / cell_size);
            int y = Mathf.FloorToInt((worldPosition.z + cell_size * 0.5f) / cell_size); 
            return new Vector2Int(x, y);
        }
    }
}
