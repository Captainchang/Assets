using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject[] monster = new GameObject[10];
    public MonsterController monsterController;
    public MonsterStat monsterstat;
    public void respawn()
    {
        gameObject.transform.position = monsterController.startPosition;
        gameObject.SetActive(true);

    }
    public void isDead()
    {
        if (monsterstat.HP <= 0)
        {
            gameObject.SetActive(false);
            monsterstat.HP = monsterstat.MaxHp;
            Invoke("respawn", 3f);
        }
    }

    void Start()
    {
         monsterController= GetComponent<MonsterController>();
    }

    
    void Update()
    {
        isDead();
    }
}
