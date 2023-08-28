using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPbar : Hpbar
{
    Define.UI_GameObjects _uitype = Define.UI_GameObjects.Hpbar;

    float Hp;
    public PlayerController player;
    [SerializeField]
    TextMeshProUGUI moncurrentHp;

    void Update()
    {
        var monsterhp = player.GetLocktarget();
        Hp = monsterhp.HP / (float)monsterhp.MaxHp;
        moncurrentHp.text = monsterhp.HP.ToString();
        moncurrentHp.color = Color.white;
        SetHpRatio(Hp);
    }
}
