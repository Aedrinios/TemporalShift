using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    public Vector2 Move { get; private set; }

    [field: SerializeField] public float JumpBuffer { get; private set; }
    [SerializeField] private float jumpBufferDuration;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        jumpAction.performed += OnJump;
        //    jumpAction.canceled += OnJump;
    }

    private void Update()
    {

        JumpBuffer = Mathf.Clamp(JumpBuffer - Time.deltaTime, 0, jumpBufferDuration);
    }


    private void OnDisable()
    {
        moveAction.performed -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        JumpBuffer = jumpBufferDuration;

    }
}