using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PopupUI : MonoBehaviour,IPointerDownHandler
{
    public Button CloseButton;
    public event Action OnFocus;
    
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnFocus();
    }
}
