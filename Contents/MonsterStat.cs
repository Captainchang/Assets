using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    [SerializeField]
    Define.MonsterType _monster = Define.MonsterType.None;

    void Start()
    {
        switch (_monster)
        {
            case Define.MonsterType.Slime:
                _hp = 50;
                _maxhp= 50;
                _attack = 2;
                break;
            case Define.MonsterType.Turtle:
                _hp = 60;
                _maxhp = 60;
                _attack = 3;
                break;
        }
    }

}
