using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.SceneView;

public class Menu : MonoBehaviour
{
    GameObject menu;
    bool isTabactive;
    CameraMove cameramove;
    Vector3 startposion;

    CameraMove.UItype _ui = CameraMove.UItype.None;
    void Start()
    {
        
        cameramove = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();
        _ui = CameraMove.UItype.Play;
       // menu.SetActive(false);
        isTabactive = false;
        startposion = gameObject.transform.localPosition;
    }

    void Tab()
    {
        //To do  플레이어 이동, 마우스 카메라 제어 둘다 막기
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
    public void Click(GameObject gameObject)
    {
        if (gameObject.activeSelf == true)
        {

            gameObject.SetActive(false);
        }
        else if (gameObject.activeSelf == false)
        {
            gameObject.transform.localPosition = startposion;

            gameObject.SetActive(true);
        }
    }
}
