using Mono.Cecil;
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
    [SerializeField]
    AudioClip attacksounds;
    [SerializeField]
    AudioClip skill1sounds;
    [SerializeField]
    AudioClip skill2sounds;
    [SerializeField]
    AudioClip skill3sounds;
    [SerializeField]
    AudioClip nonattacksounds;


    [Header("이동")]
    [Space (10)]
    public float gravity = -9.18f;

    [Header("이펙트")]
    [Space(10)]
    public GameObject[] skilllist;

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
    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //monsterController=  GameObject.FindGameObjectWithTag("Monster").GetComponent<MonsterController>();
        monsterStat = GameObject.FindGameObjectWithTag("Monster").GetComponent<MonsterStat>();
        anim = GetComponent<Animation>();
        animator = GetComponent<Animator>();
        _player = GetComponent<PlayerStat>();
        _monster =GetComponent<MonsterStat>();

        PlayerStatUI.Instance.UpdateMaxHp();
        PlayerStatUI.Instance.UpdateHp();
        PlayerStatUI.Instance.UpdateAttack();
        PlayerStatUI.Instance.UpdateCurrentHpbar();

        attackStage = AttackStage.None;
        leftfootstep = Resources.Load<AudioClip>("Sounds/Footstep/Walk1");
        rightfootstep = Resources.Load<AudioClip>("Sounds/Footstep/Walk2");
        attacksounds = Resources.Load<AudioClip>("Sounds/Attack/Attack");

        skill1sounds = Resources.Load<AudioClip>("Sounds/SkillSound/Skill1");
        skill2sounds = Resources.Load<AudioClip>("Sounds/SkillSound/Skill2");
        skill3sounds = Resources.Load<AudioClip>("Sounds/SkillSound/Skill3");

        nonattacksounds = Resources.Load<AudioClip>("Sounds/Attack/NonAttack");

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
    void AttackSounds()
    {
        AudioSource.PlayClipAtPoint(attacksounds, Camera.main.transform.position);
    }
    void Skill1Sound()
    {
        AudioSource.PlayClipAtPoint(skill1sounds, Camera.main.transform.position);
    }
    void Skill2Sound()
    {
        AudioSource.PlayClipAtPoint(skill2sounds, Camera.main.transform.position);
    }
    void Skill3Sound()
    {
        AudioSource.PlayClipAtPoint(skill3sounds, Camera.main.transform.position);
    }

    void Skill1()
    {
        Skill1Sound();
        skilllist[0].SetActive(true);
        Attack(4);
        StartCoroutine(SkillEffectDeactivate(skilllist[0], 0.5f));
    }
    void Skill2()
    {
        Skill2Sound();
        skilllist[1].SetActive(true);
        Attack(2);
        StartCoroutine(SkillEffectDeactivate(skilllist[1], 0.3f));
    }
    void Skill3()
    {
        Skill3Sound();
        skilllist[2].SetActive(true);
        StartCoroutine(SkillEffectDeactivate(skilllist[2], 0.1f));
        Attack(5);
        skilllist[3].SetActive(true);
        StartCoroutine(SkillEffectDeactivate(skilllist[3], 0.3f));
    }

    IEnumerator SkillEffectDeactivate(GameObject effectObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        effectObject.SetActive(false);

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
           
            yield return new WaitForSeconds(0.2f); // 첫 번째 공격 시간
            attackStage = AttackStage.Attack1;
        }
        else if (attackStage == AttackStage.Attack1)
        {
            animator.SetTrigger("Attack2");
            yield return new WaitForSeconds(0.2f); // 두 번째 공격 시간
            attackStage = AttackStage.Attack2;
        }
        else if (attackStage == AttackStage.Attack2)
        {
            animator.SetTrigger("Attack3");
            yield return new WaitForSeconds(0.2f); // 세 번째 공격 시간
            attackStage = AttackStage.None;
        }

        _state = Define.Player.Idle;
        yield return new WaitForSeconds(0.5f);

        isAttacking = false; // 공격이 끝났으므로 초기화
    }

    void Attack(int attack)
    {
        if (locktarget != null)
        {
            var dis = (locktarget.transform.position - gameObject.transform.position).magnitude;

            if (dis <= 2.6 )
            {
                if (monsterController == null)
                {
                    monsterController = locktarget.GetComponent<MonsterController>();
                }
                if (monsterController != null)
                {
                    monsterController.Hit(attack);
                }
            }
                    monsterController = null;
        }
    }
    void Attack()
    {
        if (locktarget != null)
        {
            var dis = (locktarget.transform.position - gameObject.transform.position).magnitude;

            if (dis <= 2.6)
            {
                if (monsterController == null)
                {
                    monsterController = locktarget.GetComponent<MonsterController>();
                }
                if (monsterController != null)
                {
                    monsterController.Hit();
                }
            }
            monsterController = null;
        }
    }
    void OnPRanimFinish()
    {
        Attack();
        _state = Define.Player.Idle;
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
        PlayerStatUI.Instance.UpdateAttack();

        //플레이어 이동
        var x = Input.GetAxis("Horizontal");   // 수평 이동
        var z = Input.GetAxis("Vertical");   // 수직

        Vector3 move = new Vector3(x, 0, z);
        move = transform.TransformDirection(move);
        controller.Move(move * _player.MoveSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        var playerForward = transform.forward;
        var rayDir = playerForward * 10f;
        Debug.DrawRay(this.transform.position + Vector3.up, rayDir, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position + Vector3.up, rayDir, out hit, 20.0f, Monster_layer))
        {
            monsterHP.SetActive(true);
            locktarget = hit.transform.gameObject;
        }
        else
        {
            monsterHP.SetActive(false);
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

        if (z != 0 && !isAttacking)
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

            if (Input.GetKeyDown(KeyCode.E) && _player.Stamina >= 20)
            {
                if (z == 0 && x == 0)
                    return;   
                controller.Move(move * _player.MoveSpeed * 3 * Time.deltaTime);

                animator.SetTrigger("Roll");
                _player.MoveSpeed = 30f;
                _player.Stamina -= 20;

            }

            if (x != 0)
            {
                x = 0;
                animator.SetFloat("RightSpeed", 0f);
            }
            animator.SetFloat("Speed", 5.0f);
        }
        if (x != 0 && !isAttacking)
        {
            if (!action)
                return;
            _player.MoveSpeed = 4f;
            if (z != 0)
            {
                z = 0;
                animator.SetFloat("Speed", 0f);
            }
            animator.SetFloat("RightSpeed", 5.0f);
        }
        if (z == 0 && x == 0)
        {
            _player.MoveSpeed = 0f;
            animator.SetFloat("Speed", 0f);
            animator.SetFloat("RightSpeed", 0f);
        }
 
    }
}