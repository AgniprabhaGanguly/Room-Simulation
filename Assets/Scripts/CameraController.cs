using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    [SerializeField] private float moveSpeed;
    private Vector2 mouseInput;
    private float pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Mouse.current != null)
        {
            // Read the raw physical movement of the mouse laser this frame
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            transform.Rotate(Vector3.up, mouseDelta.x * sensitivity * Time.deltaTime);
            pitch -= mouseDelta.y * sensitivity * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, -90f, 90f);
            transform.localEulerAngles = new Vector3(pitch, transform.localEulerAngles.y, 0f);
        }
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked; 
        }

        if (Keyboard.current.qKey.isPressed)
        {
            transform.position -= transform.up * moveSpeed * Time.deltaTime;
        }

        if (Keyboard.current.eKey.isPressed)
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        }

        if (Keyboard.current.wKey.isPressed)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        }
        
        if (Keyboard.current.aKey.isPressed)
        {
            transform.position -= transform.right * moveSpeed * Time.deltaTime;
        }
        
        if (Keyboard.current.dKey.isPressed)
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
    }
}
