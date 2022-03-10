using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float Speed = 5;
    private const float BoostMult = 1.5f;
    private const float MouseSensitivity = 500;

    float xRotation = 0;
    float yRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleAiming();
    }

    void HandleMovement()
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

        moveVector = moveVector.normalized * Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
            moveVector *= BoostMult;
        transform.Translate(Vector3.forward * moveVector.y);
        transform.Translate(Vector3.right * moveVector.x);
        transform.Translate(Vector3.up * moveVector.z);
    }

    void HandleAiming()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        }

        if (Cursor.lockState == CursorLockMode.None)
            return;

        Vector2 mouseVector = new Vector2();

        mouseVector.x = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        mouseVector.y = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;


        xRotation = Mathf.Clamp(xRotation - mouseVector.y, -90, 90);
        yRotation += mouseVector.x;


        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
