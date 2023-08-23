using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class MonsterController : MonoBehaviour
{
    Define.Monster _state = Define.Monster.None;
    Define.MonsterType _type = Define.MonsterType.None;
    Define.Monsteraction _action = Define.Monsteraction.Move;

    [SerializeField]
    private PlayerController player;
    [SerializeField]
    PlayerStat playerStat;
    [SerializeField]
    GameObject dmgtext;

    [SerializeField]
    MonsterStat monsterstat;
    [SerializeField]
    private GameObject locktarget;
    public GameObject PlayerTrans;
    public int nextmove;
    public Animator animator;
    public bool isDie = false;
    BoxCollider boxCollider;

    [SerializeField]
    public Define.MonsterType type;
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
        nextmove = 3;

    }
    void Start()
    {
        playerStat = PlayerTrans.GetComponent<PlayerStat>();
        monsterstat = GetComponent<MonsterStat>();
        player = PlayerTrans.GetComponent<PlayerController>();
        animator =GetComponent<Animator>();
        boxCollider= gameObject.GetComponent<BoxCollider>();
    }
    void UpdateIdle()
    {
        var isidle = animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        //animator.CrossFade("Idle",0.5f); TO DO

        if (locktarget == null)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, startPosition - new Vector3(nextmove,0,0), movespeed * Time.deltaTime);
        }
    }

    void DamageText()
    {
        //GameObject damageText = Instantiate(dmgtext,canvas.transform);
       // damageText.transform.position = transform.position;
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

            //미노타우루스 특수패턴 , 컨트롤러 분리 해야함 
          /*  if (playerdis > 5)
            {
                animator.SetTrigger("Kick");
                transform.position = Vector3.MoveTowards(transform.position, locktarget.transform.position - new Vector3(1,0,1), 1f);
            }*/
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
        Vector3 lookdis = locktarget.transform.position - transform.position;
        lookdis.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookdis);
        transform.rotation = rotation;

        //this.transform.LookAt(locktarget.transform);
        animator.SetBool("Attack", true);
        //animator.CrossFade("Attack2",0.5f);
    }
    public void Attack()
    {
        playerStat.HP -= monsterstat.Attack;
        player.TakingDamage();
        Debug.Log("공격! 플레이어 체력 " + playerStat.HP);
        if(playerStat.HP <= 0)
        {
            Debug.Log("죽음 ");
            //플레이어 죽음 처리 .
        }
    }
    virtual public void Hit()
    { 
        if (isDie == false)
        {
            _state = Define.Monster.Hit;
           // monsterstat = monstergameObject.GetComponent<MonsterStat>();
            switch (type)
            {
                case Define.MonsterType.Turtle :
                    DamageText();
                    monsterstat.HP -= playerStat.Attack *2;
                    break;
                case Define.MonsterType.minotaur:
                    DamageText();
                    monsterstat.HP -= playerStat.Attack * 1/2;
                    var num =  Random.Range(1, 11); //num를 플레이어 블락 능력치로 만들기
                    if(num > 5)
                        animator.SetBool("Hit",true);
                    break;
                default:
                    DamageText();
                    monsterstat.HP -= playerStat.Attack;
                    animator.SetBool("Hit", true);
                    break;
            }
            Debug.Log("공격! 몬스터 체력 " + monsterstat.HP);
            UpdateDead();
        }
    }
    public void HitAnimationend()
    {
        animator.SetBool("Hit", false);
    }

    void Respawn()
    {
        isDie = false;
        gameObject.transform.position = startPosition;
        locktarget = null;
        boxCollider.enabled = true;
        gameObject.SetActive(true);
        
        _state = Define.Monster.Idle;
    }
    IEnumerator Dead()
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }
  
    public void UpdateDead()
    {
       if( monsterstat.HP <= 0)
        {
            isDie = true;
            locktarget = null;
            _state = Define.Monster.Dead;
            animator.SetBool("isDead", true);

            StartCoroutine("Dead");
            monsterstat.HP = monsterstat.MaxHp;
            Invoke("Respawn", 10f);
        }
    }
   /* private void OnCollisionEnter(Collision collision)
    {
       if(locktarget != null && isDie == false)
        {
            Attack();
        }
    }
    공격 데미지 넣는 부분 */ 
    void Update()
    {
        
        var dis = Vector3.Distance(transform.position, PlayerTrans.transform.position);

        if (dis < 5)
        {
            _state = Define.Monster.Run;
            locktarget = PlayerTrans;
            animator.SetBool("Attack", false);
        }

        if (dis <= 2)
        {
            _state = Define.Monster.Attack;
            locktarget = PlayerTrans;
        }
        //Debug.DrawRay(this.transform.position + Vector3.up, Vector3.forward * 10 , Color.red);
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
            case Define.Monster.Hit:
                Hit();
                break;
            case Define.Monster.Attack:             
                UpdateAttack();
                break;
            case Define.Monster.Dead:
                UpdateDead();
                break;
        }
    }
}
