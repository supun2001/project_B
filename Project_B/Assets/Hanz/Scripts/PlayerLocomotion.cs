using System.Collections;
using System.Collections.Generic;
using Hanz.FinalCharacterControls;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerLocomotion : MonoBehaviour, PlayerControls.IPlayerLocomotionActions
{
    public PlayerControls PlayerControls {get; private set;}
    public Vector2 MovementInput {get; private set;}
    public Vector2 LookInput {get; private set;}

    private void OnEnable()
    {
        PlayerControls = new PlayerControls();
        PlayerControls.Enable();

        PlayerControls.PlayerLocomotion.Enable();
        PlayerControls.PlayerLocomotion.SetCallbacks(this);
    }
    
    private void OnDisable()
    {
        PlayerControls.PlayerLocomotion.Disable();
        PlayerControls.PlayerLocomotion.RemoveCallbacks(this);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
        print(MovementInput);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }
}
