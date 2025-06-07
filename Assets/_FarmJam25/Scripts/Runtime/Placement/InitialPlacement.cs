using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace NJG.Runtime.Placement
{
    public class InitialPlacement : MonoBehaviour
    {
        [SerializeField] private List<GridObjectField> _gridObjects = new();
        
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

        [Button]
        private void GetAllGridObjectsInScene()
        {
            List<MonoBehaviour> allGridObjects = new List<MonoBehaviour>();
            _gridObjects.Clear();
    
            foreach (var go in GetAllGameObjectsInScene())
            {
                if (!go.TryGetComponent(out IGridObject gridObject))
                    continue;
                GridObjectField gridObjectField = new GridObjectField();
                gridObjectField.SetField(gridObject);
                _gridObjects.Add(gridObjectField);
            }
        }
        
        private List<GameObject> GetAllGameObjectsInScene()
        {
            List<GameObject> allObjects = new List<GameObject>();

            // Get all root GameObjects in the active scene
            var roots = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

            foreach (var root in roots)
            {
                allObjects.Add(root);
                GetChildrenRecursive(root.transform, allObjects);
            }

            return allObjects;
        }

        private static void GetChildrenRecursive(Transform parent, List<GameObject> result)
        {
            foreach (Transform child in parent)
            {
                result.Add(child.gameObject);
                GetChildrenRecursive(child, result);
            }
        }
    }
}