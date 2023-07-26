using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    
    public Transform playerBody;

    float xRotation = 0f;

    public bool PersonView;

    void viewChange()
    {
        if (Input.GetKeyDown(KeyCode.B) && PersonView == true)
        {
            transform.localPosition = new Vector3(0, 3.3f, -4.7f);
            PersonView = false;
        }
        else if (Input.GetKeyDown(KeyCode.B) && PersonView == false)
        {
            transform.localPosition = new Vector3(0, 1.86f, 0.4f);
            PersonView = true;
        }
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        viewChange();

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, 17f, 42f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
