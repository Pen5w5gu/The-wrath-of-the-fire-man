using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera MainCamera;
    private bool isGround = false;
    public Rigidbody2D m_rb;
    public SpriteRenderer spriteRenderer;
    public Animator anim;
    public float MoveSpeed;
    private Vector2 moveDirection;
    public float jumpForce = 300f;

    public GameController m_gc;
    void Start()
    {
        MainCamera = Camera.main;
        m_rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        AnimationController();
    }

    private void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");

        // Tính toán vị trí mới dựa trên đầu vào
        Vector3 newPosition = transform.position + new Vector3(moveX, 0, 0) * MoveSpeed * Time.deltaTime;

   

        if (moveX < 0)
        {
            spriteRenderer.flipX = true; // Quay mặt sang trái
        }
        else if (moveX > 0)
        {
            spriteRenderer.flipX = false; // Quay mặt sang phải
        }

        // Cập nhật vị trí nhân vật với giới hạn đã được áp dụng
        transform.position = newPosition;
        moveDirection = new Vector2(moveX, 0).normalized;

        // Nhảy khi nhấn phím Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumping();
        }
    }

    private void Jumping()
    {
        if (isGround == false) return;
        m_rb.AddForce(new Vector2(0f, jumpForce));
        moveDirection = new Vector2(0f, 1f);
        isGround = false;
    }
    

    private void AnimationController()
    {
        anim.SetFloat("AnimMoveX", moveDirection.x);
        anim.SetFloat("AnimMoveY", moveDirection.y);
        anim.SetBool("isGrounded", isGround);
    }

   private void OnCollisionEnter2D(Collision2D col)
    {
		if (col.gameObject.CompareTag("Deathzone"))
		{
            Time.timeScale = 0;
            Debug.Log("Endgame");
		}
        if (col.gameObject.CompareTag("WinZone"))
        {
            Time.timeScale = 0;
            Debug.Log("Win");
        }
        if (col.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    } 
}
