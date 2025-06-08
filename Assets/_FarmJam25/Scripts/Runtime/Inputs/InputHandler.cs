using System;
using UnityEngine;
using Zenject;

namespace NJG.Runtime
{
    public enum EInputMod { Gameplay, UI, None}

    public class InputHandler : ITickable, IInitializable, IDisposable, ILateTickable
    {
        #region inputs
        public Vector2 PanInput { get; private set; }
        public int RotateButtonInput { get; private set; }

        public InputState Move { get; private set; }
        public InputState Interact { get; private set; }
        public InputState EscGameplay { get; private set; }

        public InputState EscUI { get; private set; }

        #endregion

        private Keys _keys;

        private EInputMod _inputMod;

        private EInputMod _inputModBeforePause;

        public float MouseSensitivity { get; private set; } = 1f;

        private event Action _actionResetInput;
        
       public void Initialize()
       {
           _keys = new Keys();

           Move = new InputState(_keys.Gameplay.Move, ref _actionResetInput);
           Interact = new InputState(_keys.Gameplay.Interact, ref _actionResetInput); 
           EscGameplay = new InputState(_keys.Gameplay.Esc, ref _actionResetInput);
           
           EscUI = new InputState(_keys.UI.Esc, ref _actionResetInput);

           EnableGameplayMod();
       }

       public void Dispose()
       {
           _keys.Disable();
       }
       
        public void Tick()
        {
            PanInput = _keys.Gameplay.Pan.ReadValue<Vector2>();
            
            if(_keys.Gameplay.Rotate.WasPerformedThisFrame())
                RotateButtonInput = (int)_keys.Gameplay.Rotate.ReadValue<float>();
            else
                RotateButtonInput = 0;
        }
       

        public void LateTick()
        {
            _actionResetInput.Invoke();
        }

        public void EnableGameplayMod()
        {
            _keys.Gameplay.Enable();
            _keys.UI.Disable();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            _inputMod = EInputMod.Gameplay;
        }
        public void EnableUIMod()
        {
            _keys.Gameplay.Disable();
            _keys.UI.Enable();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            _inputMod = EInputMod.UI;
        }

        public void EnableNoInputMod()
        {
            _keys.Gameplay.Disable();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;

            _inputMod = EInputMod.None;
        }

        public void SetMouseSensitivity(float sensitivity)
        {
            MouseSensitivity = sensitivity;
        }

        private void OnLevelStartInputMod(EInputMod inputMode)
        {
            //OpenInputMode(inputMode);
        }

        public void Pause()
        {
            _inputModBeforePause = _inputMod;
            EnableUIMod();
        }

        public void Resume()
        {
            ChangeInputMode(_inputModBeforePause);
        }

        private void ChangeInputMode(EInputMod newInputMod)
        {
            switch (newInputMod)
            {
                case EInputMod.Gameplay:
                    EnableGameplayMod();
                    break;
                case EInputMod.UI:
                    EnableUIMod();
                    break;
                case EInputMod.None:
                    EnableNoInputMod();
                    break;
            }
        }
    }
}