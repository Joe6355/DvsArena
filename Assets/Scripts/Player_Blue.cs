using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Blue : MainPlayer
{

    protected override void Start()
    {
        base.Start();
        hp = 10;
        moveSpeed = 10f;
        jumpForse = 10f;
        maxHP = 10; 
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.E))
        {
            gun.Shoot();
        }
    }

    protected override void Move()
    {
        base.Move();

        float moveInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f;
        }

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        Flip(moveInput);
    }


    protected override void Jump()
    {
        base.Jump();

        if (Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForse);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Grounded"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Grounded"))
        {
            isGrounded = false;
        }
    }
    
    public override void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0) 
        {
            Debug.Log("Игрок умер");
        }
        base.TakeDamage(damage);
    }
}
