using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace NJG.Runtime.Entities.Cameras
{
    public class CameraRotater : MonoBehaviour
    {
        [FoldoutGroup("Settings"), SerializeField]
        private float _smoothTime;
        
        [FoldoutGroup("Debug"), SerializeField, ReadOnly]
        private float _currentAngle;
        
        [FoldoutGroup("Debug"), SerializeField, ReadOnly]
        private float _desiredAngle;
        
        private InputHandler _inputHandler;

        private int _desiredRotationIndex;

        private float _smoothVelocity;

        [Inject]
        void Construct(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        private void LateUpdate()
        {
            _desiredRotationIndex = (_desiredRotationIndex + _inputHandler.RotateButtonInput) % 4;
            _desiredAngle = _desiredRotationIndex * -90;
            _currentAngle = Mathf.SmoothDampAngle(_currentAngle, _desiredAngle, ref _smoothVelocity, _smoothTime);
            transform.localRotation = Quaternion.Euler(0, _currentAngle, 0);
        }
    }
}