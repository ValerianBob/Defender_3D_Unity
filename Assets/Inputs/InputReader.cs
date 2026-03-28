using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public static InputReader Instance { get; private set; }

    private PlayerInputs _playerInputs;

    public Vector2 MousePosition {  get; private set; }
    public Vector2 MouseScroll { get; private set; }

    public bool MouseRightClick { get; private set; }

    public bool QButton {  get; private set; }
    public bool WButton { get; private set; }
    public bool EButton { get; private set; }
    public bool RButton { get; private set; }

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

        _playerInputs.Player.Skill1.performed += OnQButton;
        _playerInputs.Player.Skill2.performed += OnWButton;
        _playerInputs.Player.Skill3.performed += OnEButton;
        _playerInputs.Player.Skill4.performed += OnRButton;
    }

    private void OnDisable()
    {
        _playerInputs.Player.Move.performed -= OnMove;
        _playerInputs.Player.Scroll.performed -= OnScroll;
        _playerInputs.Player.RightClick.performed -= OnRightClick;

        _playerInputs.Player.Skill1.performed -= OnQButton;
        _playerInputs.Player.Skill2.performed -= OnWButton;
        _playerInputs.Player.Skill3.performed -= OnEButton;
        _playerInputs.Player.Skill4.performed -= OnRButton;

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

    private void OnQButton(InputAction.CallbackContext context)
    {
        QButton = true;
    }

    private void OnWButton(InputAction.CallbackContext context)
    {
        WButton = true;
    }

    private void OnEButton(InputAction.CallbackContext context)
    {
        EButton = true;
    }

    private void OnRButton(InputAction.CallbackContext context)
    {
        RButton = true;
    }

    private void LateUpdate()
    {
        MouseScroll = Vector2.zero;

        MouseRightClick = false;

        QButton = false;
        WButton = false;
        EButton = false;
        RButton = false;
    }
}
