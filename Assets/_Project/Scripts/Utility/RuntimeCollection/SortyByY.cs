using System;
using UnityEngine;

namespace NJG.Utilities
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SortyByY : MonoBehaviour
    {
        [SerializeField]
        private int _sortingOrderBase = 5000;
        [SerializeField]
        private float _offset = 0f;
        [SerializeField]
        private bool _runOnlyOnce = true;
        
        private SpriteRenderer _spriteRenderer;
        
        private void Awake () => _spriteRenderer = GetComponent<SpriteRenderer>();

        private void LateUpdate()
        {
            _spriteRenderer.sortingOrder = (int)(_sortingOrderBase - (transform.position.y * _offset) * 100);
            
            if (_runOnlyOnce)
                enabled = false;
        }
    }
}