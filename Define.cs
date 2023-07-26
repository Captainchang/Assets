using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
   
  
   public enum Player
    {
        None,
        Idle,
        Run,
        Attack,
        Jump
    }
    public enum PlayerState
    {
        None,
        Play,
        UI,
    }
    public enum Monster
    {
        None,
        Idle,
        Run,
        Attack,
    }
    public enum Monsteraction
    {
        None,
        Move,
        Think,
    }

    public enum MonsterType
    {
        None,
        Slime,
        Turtle,
    }
}
