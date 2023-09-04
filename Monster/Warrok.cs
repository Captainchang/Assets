using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Warrok : MonsterController
{
    private void Awake()
    {

        base.Awake();
        Playerfind();
    }
    private void Start()
    {
        base.Start();
        playerStat = PlayerTrans.GetComponent<PlayerStat>();

    }
    public override void Hit()
    {
        base.Hit();
    }
    protected override void UpdateRun()
    {
        base.UpdateRun();

        
    }
}
