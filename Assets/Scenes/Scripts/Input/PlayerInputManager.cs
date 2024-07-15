using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager 
{
    public CharacterInput _characterInput;
    PlayerMovement _playerMovement;

    public PlayerInputManager(PlayerMovement playerController)
    {
        _characterInput = new CharacterInput();
        _playerMovement = playerController;
    }


    public void EnabledInput()
    {
        _characterInput.Movement.Enable();
        _characterInput.Movement.Move.started += Onstarted;
        _characterInput.Movement.Move.canceled += OnCanceled;

    }

    public void DisabledInput()
    {
        _characterInput.Movement.Disable();
        _characterInput.Movement.Move.started -= Onstarted;
        _characterInput.Movement.Move.canceled -= OnCanceled;  
    }

    private void Onstarted(InputAction.CallbackContext callbackContext) => PlayerMovement.OnMove += _playerMovement.Move;
    private void OnCanceled(InputAction.CallbackContext callbackContext) => PlayerMovement.OnMove -= _playerMovement.Move;

}
