using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace NJG.Runtime.Placement
{
    public class InitialPlacement : MonoBehaviour
    {
        [SerializeField] private GridObjectField[] _gridObjects;
        
        [Inject]
        void Construct(GridManager gridManager)
        {
            foreach (var gridObject in _gridObjects)
            {
                if(gridObject.GridObject == null)
                    continue;
                
                Vector2Int gridIndex = gridManager.GetCellIndexFromPosition(gridObject.GridObject.Transform.position);
                gridManager.TryPlaceObject(gridIndex, gridObject.GridObject);
            }
        }
    }
}