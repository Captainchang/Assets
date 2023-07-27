using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
    Define.UI_GameObjects _uitype = Define.UI_GameObjects.Hpbar;

    float Hp;
    public Stat _stat;
 
    void Update()
    {

        Hp = _stat.HP / (float)_stat.MaxHp;
        SetHpRatio(Hp);

    }


    public void SetHpRatio(float ratio)
    {
        gameObject.GetComponent<Slider>().value = ratio;
    }
}
