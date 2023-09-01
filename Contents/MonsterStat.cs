using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    [SerializeField]
    protected string _name;
    [SerializeField]
    Define.MonsterType _monster = Define.MonsterType.None;
    public string Name { get { return _name; } set { _name = value; } }
    void Start()
    {
        switch (_monster)
        {
            case Define.MonsterType.Warrok:
                _name = "����";
                _hp = 500;
                _maxhp = 500;
                _attack = 20;
                break;
            case Define.MonsterType.minotaur:
                _name = "�̳�Ÿ�츣��";
                _hp = 200;
                _maxhp= 200;
                _attack = 10;
                break;
            case Define.MonsterType.Turtle:
                _name = "�Ķ��ź���";
                _hp = 60;
                _maxhp = 60;
                _attack = 3;
                break; 
        }
    }
}
