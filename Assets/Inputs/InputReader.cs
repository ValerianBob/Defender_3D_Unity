using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public static InputReader Instance { get; private set; }

    private PlayerInputs _playerInputs;

    public Vector2 MousePosition {  get; private set; }
    public Vector2 MouseScroll { get; private set; }

    public bool MouseRightClick { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _playerInputs = new PlayerInputs();
    } 
    
    private void OnEnable()
    {
        _playerInputs.Enable();

        _playerInputs.Player.Move.performed += OnMove;
        _playerInputs.Player.Scroll.performed += OnScroll;
        _playerInputs.Player.RightClick.performed += OnRightClick;
    }

    private void OnDisable()
    {
        _playerInputs.Player.Move.performed -= OnMove;
        _playerInputs.Player.Scroll.performed -= OnScroll;
        _playerInputs.Player.RightClick.performed -= OnRightClick;

        _playerInputs.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }

    private void OnScroll(InputAction.CallbackContext context)
    {
        MouseScroll = context.ReadValue<Vector2>();
    }

    private void OnRightClick(InputAction.CallbackContext context)
    {
        MouseRightClick = true;
    }

    private void LateUpdate()
    {
        MouseScroll = Vector2.zero;

        MouseRightClick = false;
    }
}
