using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.SceneView;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    bool isTabactive;
    CameraMove cameramove;

    CameraMove.UItype _ui = CameraMove.UItype.None;
    void Start()
    {
        cameramove = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();
        _ui = CameraMove.UItype.Play;
        menu.SetActive(false);
        isTabactive = false;
    }

    void Tab()
    {
        //To do  �÷��̾� �̵�, ���콺 ī�޶� ���� �Ѵ� ����
        if (Input.GetKeyDown(KeyCode.Tab) && isTabactive)
        {
            _ui = CameraMove.UItype.Play;
            menu.SetActive(false);
            isTabactive = false;
        }
        else if(Input.GetKeyDown(KeyCode.Tab) && !isTabactive)
        {
            _ui = CameraMove.UItype.UI;
            menu.SetActive(true);
            isTabactive = true;
        }
        
    }
    
    void Update()
    {

    }
}
