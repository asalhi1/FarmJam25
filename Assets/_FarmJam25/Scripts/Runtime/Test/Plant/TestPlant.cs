using NJG.Runtime.Placement;
using UnityEngine;
using Zenject;

namespace NJG.Runtime.Test
{
    public class TestPlant : MonoBehaviour, IDamagable, IGridObject
    {
        [SerializeField] private float _health = 10;
        public Transform Transform => transform;
        public Vector2Int GridIndex { get; private set; }
        
        public Vector2Int[] CellPositions { get; private set; } = new []
        {
            new Vector2Int(0,0)
        };
        
        GridManager _gridManager;

        [Inject]
        void Construct(GridManager gridManager)
        {
            _gridManager = gridManager;
        }
        
        
        
        public void PlaceOnMap(Vector2Int gridIndex, Vector3 cellPosition)
        {
            transform.position = cellPosition;
            GridIndex = gridIndex;
        }

        public void Damage(float damage, Vector3 DamageDirection, IDamageGiver damageGiver = null)
        {
            _health -= damage;
            if(_health <= 0)
                Die();
        }

        private void Die()
        {
            _gridManager.RemoveObject(this);
            Destroy(gameObject);
        }
    }
}