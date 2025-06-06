using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputState 
{
    public bool IsPressed { get; private set; }
    public bool IsHeld { get; private set; }
    public bool IsReleased { get; private set; }
    
    public event Action <bool> OnPressed;

    public InputState(InputAction inputAction, ref Action resetAction)
    {
        inputAction.started += ctx => OnStarted();
        inputAction.performed += ctx => OnPerformed();
        inputAction.canceled += ctx => OnCanceled();

        resetAction += ResetInputInfo;
    }

    public InputState(InputAction inputAction, InputAction inputAction2, ref Action resetAction)
    {
        inputAction.started += ctx => OnStarted();
        inputAction.performed += ctx => OnPerformed();
        inputAction.canceled += ctx => OnCanceled();

        inputAction2.started += ctx => OnStarted();
        inputAction2.performed += ctx => OnPerformed();
        inputAction2.canceled += ctx => OnCanceled();
        
        resetAction += ResetInputInfo;
    }

    private void OnStarted()
    {
        IsHeld = true;
        IsPressed = true;
    }

    private void OnPerformed()
    {
        IsPressed = true;
        OnPressed?.Invoke(true);
    } 

    private void OnCanceled()
    {
        IsHeld = false;
        IsPressed = false;
        IsReleased = true;
        OnPressed?.Invoke(false);
    }

    public void ResetInputInfo()
    {
        IsPressed = false;
        IsReleased = false;
    }
    
}
