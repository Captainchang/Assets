using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonsterController
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
        //float playerdis = Vector3.Distance(transform.position, locktarget.transform.position);
        /*//�̳�Ÿ��罺 Ư������ , ��Ʈ�ѷ� �и� �ؾ��� 
          if (playerdis > 5)
          {
              animator.SetTrigger("Kick");
              transform.position = Vector3.MoveTowards(transform.position, locktarget.transform.position - new Vector3(1,0,1), 3f);
          }*/
    }
}
