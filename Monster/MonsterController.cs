using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using static Define;

public class MonsterController : MonoBehaviour
{
    Define.Monster _state = Define.Monster.None;
    Define.MonsterType _type = Define.MonsterType.None;
    Define.Monsteraction _action = Define.Monsteraction.Move;
    
    private PlayerController player;
    [SerializeField]
    PlayerStat playerStat;

    MonsterStat monsterstat;
    [SerializeField]
    private GameObject locktarget;
    public GameObject PlayerTrans;
    public int nextmove;
    public Animator animator;

    [SerializeField]
    Define.MonsterType type;
    [SerializeField]
    LayerMask layer;
    [SerializeField]
    private float movespeed = 3.0f;
   
    public Vector3 startPosition;

    private void Awake()
    {
        PlayerTrans = GameObject.FindWithTag("Player");
        startPosition = transform.position;
        _state = Define.Monster.Idle;
       
    }
    void Start()
    {
        monsterstat = GetComponent<MonsterStat>();
        player = GetComponent<PlayerController>();
        animator =GetComponent<Animator>();
    }

    void UpdateIdle()
    {
        var isidle = animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");

        if (locktarget == null)
        {

            transform.position = Vector3.MoveTowards(transform.position, startPosition - new Vector3(nextmove,0,0), movespeed * Time.deltaTime);
        }
    }

    void UpdateRun()
    {
        if (locktarget != null)
        {
            animator.SetFloat("movespeed", 3.0f);
            transform.position = Vector3.MoveTowards(transform.position, locktarget.transform.position - new Vector3(1, 0, 1), movespeed * Time.deltaTime);
            this.transform.LookAt(locktarget.transform);

            float playerdis = Vector3.Distance(transform.position, locktarget.transform.position);
            //Debug.Log(maxdistance);
            if (playerdis > 15)
            {
                animator.SetFloat("movespeed", 0.0f);
                locktarget = null;
                transform.position = Vector3.MoveTowards(transform.position, startPosition, 20f);
                _state = Define.Monster.Idle;
            }
        }
    }
    void UpdateAttack()
    {
        this.transform.LookAt(locktarget.transform);
        animator.SetBool("Attack", true);
        
    }
    public void Attack()
    {
        playerStat.HP -= monsterstat.Attack;
        Debug.Log("공격! 플레이어 체력 " + playerStat.HP);
    }
    public void hit()
    {
        monsterstat.HP -= playerStat.Attack;
        Debug.Log("공격! 몬스터 체력 " + monsterstat.HP);
        isDead();
    }
    public void respawn()
    {
        gameObject.transform.position = startPosition;
        gameObject.SetActive(true);
    }
    public void isDead()
    {
       if( monsterstat.HP <= 0)
        {
            gameObject.SetActive(false);
            monsterstat.HP = monsterstat.MaxHp;
            Invoke("respawn", 3f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
       if(locktarget != null)
        {
            Attack();
        }
    }

    void Update()
    {
        
        float dis = Vector3.Distance(transform.position, PlayerTrans.transform.position);

        if (dis < 5)
        {
            _state = Define.Monster.Run;
            locktarget = PlayerTrans;
            animator.SetBool("Attack", false);
        }
        if (dis <= 2)
        {
            _state = Define.Monster.Attack;
        }
        Debug.DrawRay(this.transform.position + Vector3.up, Vector3.forward * 10 , Color.red);
        RaycastHit hit;
        
        if (Physics.Raycast(this.transform.position +Vector3.up,Vector3.forward *10 ,out hit,20.0f,layer))
        {
            locktarget = hit.transform.gameObject;
        }

        switch (_state)
        {
            case Define.Monster.Idle:
                UpdateIdle();
                break;

            case Define.Monster.Run:
                UpdateRun();
                break;

            case Define.Monster.Attack:
                UpdateAttack();
                break;
        }
    }
}
