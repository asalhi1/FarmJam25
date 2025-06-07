using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NJG.Runtime.Placement
{
    public interface IGridObject
    {
        public Transform Transform { get; }
        public Vector2Int GridIndex { get; }
        public Vector2Int[] CellPositions { get; }
        
        public void PlaceOnMap(Vector2Int gridIndex, Vector3 cellPosition);
    }
    
    [Serializable]
    public struct GridObjectField
    {
        [SerializeField, OnValueChanged(nameof(OnFieldChanged)), ValidateInput("@_gridObject is IGridObject")]
        private MonoBehaviour _gridObject;

        public IGridObject GridObject => _gridObject as IGridObject;

        public void SetField(IGridObject gridObject)
        {
            _gridObject = gridObject as MonoBehaviour;
        }

        private void OnFieldChanged()
        {
            if (_gridObject is IGridObject)
                return;

            if (_gridObject.TryGetComponent(out IGridObject placable))
                _gridObject = placable as MonoBehaviour;
            else
                _gridObject = null;
        }
    }
}