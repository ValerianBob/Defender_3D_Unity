using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    private Rigidbody _rb;

    private float _screenEdgeBorder = 10f;

    private float _scrollTopBorder = 35f;
    private float _scrollDownBorder = 5f;

    private int _cameraLeftBorder = -45;
    private int _cameraRightBorder = 175;
    private int _cameraTopBorder = 130;
    private int _cameraDownBorder = -45;

    public float MoveSpeed;
    public float ScrollSpeed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        MoveCamera();

        ScrollCamera();

        CameraMovementBorder();
    }

    private void MoveCamera()
    {
        if (InputReader.Instance.MousePosition.x > Screen.width - _screenEdgeBorder)
        {
            _rb.AddForce(Vector3.right * MoveSpeed * Time.deltaTime);
        }

        if (InputReader.Instance.MousePosition.x < _screenEdgeBorder)
        {
            _rb.AddForce(Vector3.left * MoveSpeed * Time.deltaTime);
        }

        if (InputReader.Instance.MousePosition.y > Screen.height - _screenEdgeBorder)
        {
            _rb.AddForce(Vector3.forward * MoveSpeed * Time.deltaTime);
        }

        if (InputReader.Instance.MousePosition.y < _screenEdgeBorder)
        {
            _rb.AddForce(Vector3.back * MoveSpeed * Time.deltaTime);
        }
    }

    private void ScrollCamera()
    {
        if (InputReader.Instance.MouseScroll.y < 0 && transform.position.y < _scrollTopBorder)
        {
            _rb.AddForce(Vector3.up * ScrollSpeed * Time.deltaTime);
        }
        if (InputReader.Instance.MouseScroll.y > 0 && transform.position.y > _scrollDownBorder)
        {
            _rb.AddForce(Vector3.down * ScrollSpeed * Time.deltaTime);
        }

        if (transform.position.y > _scrollTopBorder)
        {
            transform.position = new Vector3(transform.position.x, _scrollTopBorder, transform.position.z);
        }

        if (transform.position.y < _scrollDownBorder)
        {
            transform.position = new Vector3(transform.position.x, _scrollDownBorder, transform.position.z);
        }
    }

    private void CameraMovementBorder()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, _cameraLeftBorder, _cameraRightBorder);
        pos.z = Mathf.Clamp(pos.z, _cameraDownBorder, _cameraTopBorder);

        transform.position = pos;
    }
}
