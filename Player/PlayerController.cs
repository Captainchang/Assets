using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
    GameObject talkobj;
    private GameObject Npc;
    [SerializeField]
    LayerMask Monster_layer;
    [SerializeField]
    LayerMask Npc_layer;
    Texture2D Basic;


    [Header("이동")]
    [Space (10)]
    public float gravity = -9.18f;

    bool isjumping;
    public Vector3 velocity;
    float Jumpforce = 4.0f;
    public Vector3 move;
    bool action = true;
    private void Start()
    {
        //monsterController=  GameObject.FindGameObjectWithTag("Monster").GetComponent<MonsterController>();
        monsterStat = GameObject.FindGameObjectWithTag("Monster").GetComponent<MonsterStat>();
        anim = GetComponent<Animation>();
        animator = GetComponent<Animator>();
        _player = GetComponent<PlayerStat>();
        _monster =GetComponent<MonsterStat>();
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

        RaycastHit hit;

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
        if (Input.GetMouseButton(0))
        {
            if (!action)
                return;
            animator.SetBool("PR", true);
            _state = Define.Player.Attack;
        }
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
                    monsterController.hit();
                    monsterController = null;
                }
            }
        }
    }
    void OnPRanimFinish()
    {
        animator.SetBool("PR", false);
        Attack();
        _state = Define.Player.Idle;
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

        var playerForward = transform.forward;
        var rayDir = playerForward * 10f;
        Debug.DrawRay(this.transform.position + Vector3.up, rayDir, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position + Vector3.up, rayDir, out hit, 20.0f, Monster_layer))
        {
            locktarget = hit.transform.gameObject;
        }
        else
        {
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
            talkobj.SetActive(false);
        }
        //float x = Input.GetAxis("Horizontal");   // 수평 이동
        var z = Input.GetAxis("Vertical");

        var ispuuch = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");

        if (z >= 0.1f && !(ispuuch))
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
