using System;
using NJG.Runtime.Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace NJG.Prototypes
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private PlayerPawn pawn;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private InputActionReference moveAction;
        [FormerlySerializedAs("PlayerState")] [SerializeField] private PlayerState playerState;
        [SerializeField] private float raycastCorrectionDistance = 5.0f;

        public Camera GetCamera() { return playerCamera; }
        
        private void OnEnable()
        {
            moveAction.action.performed += OnClick;
            moveAction.action.Enable();
            if (pawn == null)
            {
                Debug.LogWarning($"PlayerController enabled with pawn unattached.");
            }
            else
            {
                pawn.OnAttach(this);
            }
        }

        private void OnDisable()
        {
            moveAction.action.performed -= OnClick;
            moveAction.action.Disable();
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            
            Vector2 screenPosition = Mouse.current.position.ReadValue();
            Ray ray = playerCamera.ScreenPointToRay(screenPosition);
            int terrainLayerMask = 1 << 7;
            if (Physics.Raycast(ray, out RaycastHit hit, terrainLayerMask))
            {
                NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, raycastCorrectionDistance, NavMesh.AllAreas);
                if (navHit.hit)
                {
                    bool success = pawn.GoTo(navHit.position);
                    if (!success)
                    {
                        Debug.LogWarning($"Attempted to go to {navHit.position} but GoTo returned false.");
                    }
                }
                else
                {
                    Debug.LogWarning("NavMesh not present near character (it may require baking).");
                }
            }
        }
        
        public void Attach(PlayerPawn newPawn)
        {
            pawn = newPawn;
            pawn.OnAttach(this);
        }

        public PlayerPawn Detach()
        {
            PlayerPawn old = pawn;
            pawn = null;
            pawn.OnDetach();
            return old;
        }

        public void Start()
        {
            playerState = new PlayerState();
        }
    }
}
