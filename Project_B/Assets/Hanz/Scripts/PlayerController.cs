using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera playerCamera;

    [Header("Base movement")]
    public float runAcceleration = 0.25f;
    public float runSpeed = 4f;
    public float drag = 0.1f;

    [Header("Camera Settings")]
    public float lookSenseH = 0.1f;
    public float lookSenseV = 0.1f;
    public float fixedXRotation = 35f;

    private PlayerLocomotion playerLocomotion;
    private Vector2 cameraRotation = Vector2.zero;
    private Vector2 playerTargetRotation = Vector2.zero;

    private void Awake()
    {
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        Vector3 cameraForwardXZ = new Vector3(playerCamera.transform.forward.x,0f, playerCamera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(playerCamera.transform.right.x,0f,playerCamera.transform.right.z).normalized;
        Vector3 movementDirection = cameraRightXZ * playerLocomotion.MovementInput.x + cameraForwardXZ * playerLocomotion.MovementInput.y;

        Vector3 movementDelta = movementDirection * runAcceleration * Time.deltaTime;
        Vector3 newVelocity = characterController.velocity + movementDelta;

        //Drag player
        Vector3 currentDrag = newVelocity.normalized * drag * Time.deltaTime;
        newVelocity = (newVelocity.magnitude > drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
        newVelocity = Vector3.ClampMagnitude(newVelocity,runSpeed);

        //Move character
        characterController.Move(newVelocity * Time.deltaTime);
    }

    private void LateUpdate()
    {
        // Update camera rotation based on input
        cameraRotation.x += lookSenseH * playerLocomotion.LookInput.x;
        cameraRotation.y = Mathf.Clamp(cameraRotation.y - lookSenseV * playerLocomotion.LookInput.y, -0f, 0f);

        // Update player target rotation based on input
        playerTargetRotation.x += transform.eulerAngles.x + lookSenseH * playerLocomotion.LookInput.x;
        transform.rotation = Quaternion.Euler(0f, playerTargetRotation.x, 0f);

        // Set the camera rotation to a specific value
        playerCamera.transform.rotation = Quaternion.Euler(fixedXRotation, cameraRotation.x, 0f);
    }
}
