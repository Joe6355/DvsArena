using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    public int maxHP;
    public int hp;
    public float moveSpeed;
    public float jumpForse;
    protected Rigidbody2D rb;
    public bool isGrounded;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        Move();
        Jump();
    }

    protected virtual void Move() { }

    protected virtual void Jump() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Healing"))
        {
            hp += 5;

            if (hp > maxHP)
            {
                hp = maxHP;
            }

            Destroy(other.gameObject);
        }
    }

}

