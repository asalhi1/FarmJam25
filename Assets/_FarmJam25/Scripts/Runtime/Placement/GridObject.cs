using UnityEngine;

namespace NJG.Runtime.Placement
{
    public class GridObject : MonoBehaviour, IGridObject
    {
        [field:SerializeField] 
        public Vector2Int[] CellPositions { get; private set; }
        [field:SerializeField] 
        public Vector2Int GridIndex { get; private set; }
        
        public Transform Transform => transform;
        
        public void PlaceOnMap(Vector2Int gridIndex, Vector3 cellPosition) 
        {
            transform.position = cellPosition;
            GridIndex = gridIndex;
        }

        
    }
}