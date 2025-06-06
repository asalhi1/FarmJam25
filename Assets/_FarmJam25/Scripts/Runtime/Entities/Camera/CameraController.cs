using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace NJG.Runtime.Entities.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [FoldoutGroup("Settings"),SerializeField] 
        float _panSpeed = 20f;
        [FoldoutGroup("Settings"),SerializeField] 
        float _panBorderThickness = 10f;
        [FoldoutGroup("Settings"),SerializeField] 
        float _smoothTime = 0.1f;

        private InputHandler _inputHandler;
        
        private Vector3 _camForward;
        private Vector3 _camRight;

        private Vector3 _desiredSpeed;
        private Vector3 _currentSpeed;

        private Vector3 _speedVelocity;

        [Inject]
        void Construct(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        void LateUpdate()
        {
            SetCamDirs();
            _desiredSpeed = Vector3.zero;
            HandleKeyboardPan();
            HandleMousePan();
            MoveCamera();
        }

        private void SetCamDirs()
        {
            _camForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
            _camRight = Vector3.Cross(Vector3.up, _camForward ).normalized;
        }

        private void HandleKeyboardPan()
        {
            Vector3 moveDir = (_camForward * _inputHandler.PanInput.y + _camRight * _inputHandler.PanInput.x).normalized;
    
            _desiredSpeed += moveDir * _panSpeed;
        }

        private void HandleMousePan()
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            Vector3 moveDir = Vector3.zero;

            if (mousePos.y >= Screen.height - _panBorderThickness)
                moveDir += _camForward;
            else if (mousePos.y <= _panBorderThickness)
                moveDir -= _camForward;

            if (mousePos.x >= Screen.width - _panBorderThickness)
                moveDir += _camRight;
            else if (mousePos.x <= _panBorderThickness)
                moveDir -= _camRight;

            _desiredSpeed += moveDir.normalized * _panSpeed;
        }

        private void MoveCamera()
        {
            _currentSpeed = Vector3.SmoothDamp(_currentSpeed, _desiredSpeed, ref _speedVelocity, _smoothTime);
            transform.Translate(_currentSpeed * Time.deltaTime, Space.World);
        }
    }
}