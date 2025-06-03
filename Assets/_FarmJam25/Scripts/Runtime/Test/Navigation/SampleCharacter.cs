using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class SampleCharacter : MonoBehaviour
{
    public Camera camera;
    public NavMeshAgent agent;
    public InputActionReference click;
    private void OnEnable()
    {
        click.action.performed += OnClick;
        click.action.Enable();
    }

    private void OnDisable()
    {
        click.action.performed -= OnClick;
        click.action.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = Mouse.current.position.ReadValue();

        Ray ray = camera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            agent.SetDestination(hit.point);
        }
    }
}
