using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
   
    void Start()
    {
        Invoke("hide", 1.5f);
    }
    void hide()
    {
        gameObject.SetActive(false);
    }

}
