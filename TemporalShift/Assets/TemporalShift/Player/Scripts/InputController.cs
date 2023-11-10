using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    public Vector2 Move { get; private set; }

    public bool Jump => jumpAction.triggered;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
    }


    private void OnDisable()
    {
        moveAction.performed -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }
}