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
            case Define.MonsterType.minotaur:
                _hp = 200;
                _maxhp= 200;
                _attack = 10;
                break;
            case Define.MonsterType.Turtle:
                _hp = 60;
                _maxhp = 60;
                _attack = 3;
                break;
            
        }
    }

}
