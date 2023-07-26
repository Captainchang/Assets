using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Define.Player _state = Define.Player.Idle;

    PlayerStat _player;
    public CharacterController controller;
    public Animation anim;
    public Animator animator;

    [Header("�̵�")]
    [Space (10)]
    public float gravity = -9.18f;

    bool isjumping;
    public Vector3 velocity;
    float Jumpforce = 4.0f;
    public Vector3 move;
    private void Start()
    {
        anim = GetComponent<Animation>();
        animator = GetComponent<Animator>();
        _player = GetComponent<PlayerStat>();
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && !isjumping)
        {
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
    void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("PR", true);
            _state = Define.Player.Attack;
        }
    }
    void OnPRanimFinish()
    {
        animator.SetBool("PR", false);
        _state = Define.Player.Idle;
    }
    private void FixedUpdate()
    {
        Jump();
    }
    private void Update()
    {
        Attack();
        //float x = Input.GetAxis("Horizontal");   // ���� �̵�
        float z = Input.GetAxis("Vertical");

        if (z >= 0.1f && !(animator.GetCurrentAnimatorStateInfo(0).IsName("PunchR")))
        {
           _state = Define.Player.Run;
           _player.MoveSpeed = 4f;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _player.MoveSpeed = 7f;
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

       controller.Move(velocity * Time.deltaTime);
    }
}