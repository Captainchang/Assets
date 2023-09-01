using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class MonsterHPbar : Hpbar
{
    Define.UI_GameObjects _uitype = Define.UI_GameObjects.Hpbar;

    float Hp;
    public PlayerController player;
    [SerializeField]
    TextMeshProUGUI moncurrentHp;
    [SerializeField]
    TextMeshProUGUI monName;

    void Update()
    {
        var monster = player.GetLocktarget();
        Hp = monster.HP / (float)monster.MaxHp;
        moncurrentHp.text = monster.HP.ToString();
        monName.text = monster.Name.ToString();
        moncurrentHp.color = Color.white;
        SetHpRatio(Hp);
    }
}
