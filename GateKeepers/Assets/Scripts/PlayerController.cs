using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float gravity = -9.81f; 

    [Header("References")]
    public CharacterController characterController;
    public Camera cam;

    private Vector3 velocity;
    private PlayerInput playerInput;
    private InputAction moveAction;

    void Start()
    {
        // components
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        // Setup input
        if (playerInput != null && playerInput.actions != null)
        {
            moveAction = playerInput.actions.FindAction("Move");
        }

        // Auto-find camera
        if (cam == null)
        {
            cam = Camera.main;
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (PauseControl.IsGamePaused)
        {
            return;
        }
        // Get input
        Vector2 input = Vector2.zero;
        if (moveAction != null)
        {
            input = moveAction.ReadValue<Vector2>();
        }
        else
        {
            // Fallback input
            input = new Vector2(
                (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0),
                (Keyboard.current.wKey.isPressed ? 1 : 0) - (Keyboard.current.sKey.isPressed ? 1 : 0)
            );
        }

        // CAMERA-RELATIVE MOVEMENT
        Vector3 move = CalculateCameraRelativeMovement(input);
        characterController.Move(move * speed * Time.deltaTime);

        // Apply gravity
        ApplyGravity();
    }

    private Vector3 CalculateCameraRelativeMovement(Vector2 input)
    {
        if (cam == null) return new Vector3(input.x, 0, input.y);

        // Get camera forward and right directions (ignore Y for ground movement)
        Vector3 cameraForward = cam.transform.forward;
        Vector3 cameraRight = cam.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Combine input with camera direction
        Vector3 moveDirection = (cameraForward * input.y) + (cameraRight * input.x);
        return moveDirection.normalized;
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
    }
}