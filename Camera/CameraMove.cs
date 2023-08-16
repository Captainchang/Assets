using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using static CameraMove;

public class CameraMove : MonoBehaviour
{
    public GameObject menu;
    bool isTabactive;
    PlayerController playerController;
    public enum UItype
    {
        None,
        UI,
        Play,
        Talk,
    }
    UItype uitype = UItype.None;
   
    public float mouseSensitivity = 100f;
    
    public Transform playerBody;

    float xRotation = 0f;

    bool PersonView;

    private void Start()
    {

        playerController= GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        menu.SetActive(false);
        isTabactive = false;
        uitype = UItype.Play;
    }
    void ViewChange()
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
    public void MovingCamera()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerController.Inaction();
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, 17f, 42f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
    public void LockCamera()
    {
        Cursor.lockState = CursorLockMode.None;
        playerController.Dontmove();

        float mouseX = 0;
        float mouseY = 0;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, 17f, 42f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void Tab()
    {
        //To do  플레이어 이동, 마우스 카메라 제어 둘다 막기
        if (Input.GetKeyDown(KeyCode.Tab) && uitype == UItype.UI)
        {
            uitype =UItype.Play;
            menu.SetActive(false);
            isTabactive = false;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && uitype == UItype.Play)
        {
            uitype = UItype.UI;
            menu.SetActive(true);
            isTabactive = true;
        }
    }
    public void TypeUI()
    {
        uitype = UItype.Talk;
        transform.localPosition = new Vector3(0, 1.86f, 0.4f);
        PersonView = true;
    }
    public void TypePlay()
    {
        uitype= UItype.Play;
        transform.localPosition = new Vector3(0, 3.3f, -4.7f);
        PersonView = false;
    }
    void Update()
    {
        ViewChange();
        Tab();
        switch(uitype)
        {
            case UItype.UI:
                LockCamera();
                break;
            case UItype.Play:
                MovingCamera();
                break;
            case UItype.Talk:
                LockCamera();
                break;

        }
    }
}
