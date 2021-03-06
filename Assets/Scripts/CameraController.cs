using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     A first person controller for the camera. Allows free movement across all three axes and rotation<br />
///     across the x and y axes. The camera will move in relation to where the camera is aiming (ie: moving<br />
///     forward while looking down will move the camera down).<br />
///     <br />
///     Camera rotation on the X axis is locked from -90deg to 90deg, preventing the camera from looking "upside down".
/// </summary>
public class CameraController : MonoBehaviour
{
    private const float Speed = 5;
    private const float BoostMult = 1.5f;
    private const float MouseSensitivity = 500;

    private float xRotation = 0;
    private float yRotation = 0;

    public bool Controllable { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        this.Controllable = true;
        this.xRotation = transform.rotation.eulerAngles.x;
        this.yRotation = transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.Controllable)
        {
            return;
        }

        HandleInput();
        HandleAiming();
    }

    /// <summary>
    ///     Handles input for the camera, including movement and mouse control.
    /// </summary>
    void HandleInput()
    {
        Vector3 moveVector = new Vector2(); 

        if (Input.GetKey(KeyCode.W))
        {
            moveVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVector.x += 1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            moveVector.z += 1;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            moveVector.z -= 1;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        }

        moveVector = Speed * Time.deltaTime * moveVector.normalized;
        if (Input.GetKey(KeyCode.LeftShift))
            moveVector *= BoostMult;

        transform.Translate(Vector3.forward * moveVector.y);
        transform.Translate(Vector3.right * moveVector.x);
        transform.Translate(Vector3.up * moveVector.z);
    }

    /// <summary>
    ///     Handles camera aiming and controlling, only allowing the camera to look around while the cursour is locked<br />
    ///     to the screen.
    /// </summary>
    void HandleAiming()
    {
        if (Cursor.lockState == CursorLockMode.None)
            return;

        Vector2 mouseVector = new Vector2()
        {
            x = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime,
            y = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime,
        };

        this.xRotation = Mathf.Clamp(xRotation - mouseVector.y, -90, 90);
        this.yRotation += mouseVector.x;


        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
