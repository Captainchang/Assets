using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{
    Define.UI_GameObjects _uitype = Define.UI_GameObjects.Staminabar;

    float Stamina;
    public Stat _stat;

    void Update()
    {
        Stamina = _stat.Stamina / (float)_stat.MaxStamina;
        SetStaminaRatio(Stamina);

    }

    public void SetStaminaRatio(float ratio)
    {
        gameObject.GetComponent<Slider>().value = ratio;
    }
}
