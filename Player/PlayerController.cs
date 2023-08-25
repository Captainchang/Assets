using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    public delegate void BlockDelegate();
    public static event BlockDelegate OnBlock;

    Define.Player _state = Define.Player.Idle;
    [SerializeField]
    QuestManager questManager;
    MonsterController monsterController;
    MonsterStat monsterStat;
    [SerializeField]
    PlayerStat _player;
    [SerializeField]
    MonsterStat _monster;
    public CharacterController controller;
    public Animation anim;

    public Animator animator;
    [SerializeField]
    private GameObject locktarget;
    [SerializeField]
    GameObject monsterHP;

    [SerializeField]
    GameObject talkobj;
    private GameObject Npc;
    [SerializeField]
    LayerMask Monster_layer;
    [SerializeField]
    LayerMask Npc_layer;
    Texture2D Basic;

    [Header("소리")]
    [Space(10)]
    [SerializeField]
    AudioClip leftfootstep;
    [SerializeField]
    AudioClip rightfootstep;


    [Header("이동")]
    [Space (10)]
    public float gravity = -9.18f;

    bool isjumping;
    public Vector3 velocity;
    float Jumpforce = 4.0f;
    public Vector3 move;
    bool action = true;

    enum AttackStage 
    {
        None,
        Attack1,
        Attack2,
        Attack3
    }

    AttackStage attackStage = AttackStage.None;
    bool isAttacking = false;
    float attackCooldown = 1.0f;
    float lastAttackTime = 0.0f;
    private void Start()
    {
        //monsterController=  GameObject.FindGameObjectWithTag("Monster").GetComponent<MonsterController>();
        monsterStat = GameObject.FindGameObjectWithTag("Monster").GetComponent<MonsterStat>();
        anim = GetComponent<Animation>();
        animator = GetComponent<Animator>();
        _player = GetComponent<PlayerStat>();
        _monster =GetComponent<MonsterStat>();

        attackStage = AttackStage.None;
        leftfootstep = Resources.Load<AudioClip>("Sounds/Footstep/Walk1");
        rightfootstep = Resources.Load<AudioClip>("Sounds/Footstep/Walk2");

    }
    public void Dontmove()
    {
        //Todo 플레이어 입력막기
        action = false;
        isjumping = true;
        animator.SetBool("Jump", false);
        _player.MoveSpeed = 0f;
        animator.SetFloat("Speed", 0f);
    }
    void UpdateMouseCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Basic = Resources.Load<Texture2D>("Cursor/Basic");
        Cursor.SetCursor(Basic, new Vector2(Basic.width / 3, 0), CursorMode.Auto);
    }
    public void Inaction()
    {
        action = true;
    }
    void Npc_quest()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine("TalkStart");
        }
    }
    IEnumerator TalkStart()
    {
        questManager.Action(Npc);
        yield return new WaitForSeconds(0.1f);
    }
    void Leftfoot()
    {
        AudioSource.PlayClipAtPoint(leftfootstep, Camera.main.transform.position);
    }
    void Rightfoot()
    {
        AudioSource.PlayClipAtPoint(rightfootstep, Camera.main.transform.position);
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && !isjumping)
        {
            if (!action)
                return;
            isjumping = true;
            _state = Define.Player.Jump;
            velocity.y = Jumpforce;
            animator.SetBool("Jump", true);
        }
        if(controller.isGrounded)
        {
            isjumping = false;
            animator.SetBool("Jump", false);
        }
    }
    void Block()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!action)
                return;
            animator.SetBool("Block", true);
            OnBlock();
        }
    }
    void OnPRanim()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                if (!action)
                    return;
                if (!isAttacking)
                {
                    isAttacking = true;
                    StartCoroutine(AttackCoroutine()); 
                }

                lastAttackTime = Time.time; 
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        if (attackStage == AttackStage.None)
        {
            animator.SetTrigger("Attack");
            _state = Define.Player.Attack;
            yield return new WaitForSeconds(0.1f); // 첫 번째 공격 시간
            attackStage = AttackStage.Attack1;
        }
        else if (attackStage == AttackStage.Attack1)
        {
            animator.SetTrigger("Attack2");
            yield return new WaitForSeconds(0.1f); // 두 번째 공격 시간
            attackStage = AttackStage.Attack2;

        }
        else if (attackStage == AttackStage.Attack2)
        {
            animator.SetTrigger("Attack3");
            yield return new WaitForSeconds(0.1f); // 세 번째 공격 시간
            attackStage = AttackStage.None;

        }

        _state = Define.Player.Idle;
        yield return new WaitForSeconds(0.5f);

        isAttacking = false; // 공격이 끝났으므로 초기화
    }
    public void TakingDamage()
    {
        // animator.SetTrigger("Hit");
      //  animator.CrossFade("Hit", 0.5f);
    }
    void Attack()
    {
        if (locktarget != null)
        {
            var dis = (locktarget.transform.position - gameObject.transform.position).magnitude;

            if (dis <= 2 )
            {
                if (monsterController == null)
                {
                    monsterController = locktarget.GetComponent<MonsterController>();
                }
                if (monsterController != null)
                {
                    monsterController.Hit();
                    monsterController = null;
                }
            }
        }
    }

    void OnPRanimFinish()
    {
        Attack();
        _state = Define.Player.Idle;
    }
    void BlockFinish()
    {
        animator.SetBool("Block", false);
    }
    public MonsterStat GetLocktarget()
    {
        return locktarget.GetComponent<MonsterStat>();
    }
    void Stamina()
    {
        if (_player.Stamina <= _player.MaxStamina && _state != Define.Player.Run)
            _player.Stamina += 7 * Time.deltaTime;
    }
    private void FixedUpdate()
    {
        Jump();
    }
    private void Update()
    {
        UpdateMouseCursor();
        Stamina();
        OnPRanim();
        Block();

        var playerForward = transform.forward;
        var rayDir = playerForward * 10f;
        Debug.DrawRay(this.transform.position + Vector3.up, rayDir, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position + Vector3.up, rayDir, out hit, 20.0f, Monster_layer))
        {
            monsterHP.SetActive(true);
            animator.SetBool("Combat",true);
            locktarget = hit.transform.gameObject;
        }
        else
        {
            monsterHP.SetActive(false);
            animator.SetBool("Combat", false);
            locktarget = null;
        }

        if (Physics.Raycast(this.transform.position + Vector3.up, rayDir, out hit, 20.0f, Npc_layer))
        {
            Npc = hit.transform.gameObject;
            talkobj.SetActive(true);
            Npc_quest();
        }
        else
        {
            Npc = null;
            talkobj.SetActive(false);
        }
        //float x = Input.GetAxis("Horizontal");   // 수평 이동
        var z = Input.GetAxis("Vertical");

       // var ispuuch = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");

        if (z >= 0.1f && !(isAttacking))
        {
            if (!action)
                return;
           _state = Define.Player.Walk;
           _player.MoveSpeed = 4f;
            if (Input.GetKey(KeyCode.LeftShift) && _player.Stamina >= 10)
            {
                _state = Define.Player.Run;
                _player.MoveSpeed = 7f;
                _player.Stamina -= 20 * Time.deltaTime; 
            }
            else
            {
                _state = Define.Player.Idle;
            }
           animator.SetFloat("Speed", 5.0f);
        }
        else
        {
            _player.MoveSpeed = 0f;
            animator.SetFloat("Speed", 0f);
        }
        move =  transform.forward * z;

        controller.Move(move * _player.MoveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

       controller.Move(velocity* Time.deltaTime);
    }
}
