using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPbar : Hpbar
{
    Define.UI_GameObjects _uitype = Define.UI_GameObjects.Hpbar;

    float Hp;
    public PlayerController player;

    void Update()
    {
        var monsterhp = player.GetLocktarget();
        Hp = monsterhp.HP / (float)monsterhp.MaxHp;
        SetHpRatio(Hp);
    }
}
