using NJG.Runtime.Placement;
using UnityEngine;
using Sirenix.OdinInspector;
using Zenject;

public class TestHouseSpawner : MonoBehaviour
{
    [SerializeField] GridObject _gridObject;

    [SerializeField] private float _maxRadiusForRandomPoint = 20;
    
    private GridManager _gridManager;

    [Inject]
    void Construct(GridManager gridManager)
    {
        _gridManager = gridManager;
    }

    [Button]
    private void Spawn10Houses()
    {
        for (int i = 0; i < 10; i++)
        {
            GridObject gridObject = Instantiate(_gridObject, GetRandomPosition(), Quaternion.identity);

            while (!_gridManager.TryPlaceObject(_gridManager.GetCellIndexFromPosition(gridObject.transform.position), gridObject))
                gridObject.transform.position = GetRandomPosition();
        }
    }
    
    private Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _maxRadiusForRandomPoint;
        randomDirection += transform.position;
        randomDirection.y = 0;
        return randomDirection;
    }
}
