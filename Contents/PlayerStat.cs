using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;


    public int Exp { get { return _exp; } set { _exp = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }

    public void Start()
    {
        _level = 1;
        _hp = 200;
        _maxhp = 200;
        _attack = 50;
        _defense = 5;
        _stamina = 100;
        _maxstamina= 100;
        _exp = 0;
        _gold =0;
    }
}
