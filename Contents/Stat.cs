using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{

    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxhp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    [SerializeField]
    protected float _moveSpeed;
    [SerializeField]
    protected float _stamina;
    [SerializeField]
    protected float _maxstamina;

    public int Level { get { return _level; } set { _level = value; } }
    public int HP { get { return _hp;} set { _hp = value; } }
    public int MaxHp { get { return _maxhp; } set { _maxhp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float Stamina { get { return _stamina; } set { _stamina = value; } }
    public float MaxStamina { get { return _maxstamina; } set { _maxstamina = value; } }

    public void Awake()
    {
        _level = 1;
        _hp = 100;
        _maxhp = 100;
        _attack = 10;
        _defense = 5;
        _stamina = 100;
        _maxstamina = 100;
    }
}
