using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera MainCamera;
    private bool isGround = false;
    public Rigidbody2D m_rb;
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

        // Tính toán giới hạn của màn hình
        float screenHalfWidth = MainCamera.orthographicSize * MainCamera.aspect; // Bán kính chiều ngang
        float screenHalfHeight = MainCamera.orthographicSize; // Bán kính chiều dọc

        // Giới hạn vị trí X và Y sau khi tính toán vị trí mới
        float clampedX = Mathf.Clamp(newPosition.x, -screenHalfWidth, screenHalfWidth);
        float clampedY = Mathf.Clamp(newPosition.y, -screenHalfHeight, screenHalfHeight);

        // Cập nhật vị trí nhân vật với giới hạn đã được áp dụng
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
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
        /*if (col.gameObject.CompareTag("Enemy"))
        {
            m_gc.m_health = (m_gc.m_health - 1);
            //m_gc.spawnKillEffect(col.gameObject.transform.position);
            Destroy(col.gameObject);
        }*/
        if (col.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    } 
}
