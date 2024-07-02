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

    public MainGun [] guns;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        Move();
        Jump();
    }

    protected virtual void Move() {
        
    }

    protected virtual void Jump() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Обработка события взаимодействия с вещами типа "Healing"
        if (other.CompareTag("Healing"))
        {
            hp += 5;

            if (hp > maxHP)
            {
                hp = maxHP;
            }

            Destroy(other.gameObject);
        }

        // Обработка смены оружия
        switch (other.tag)
        {
            case "Ak47":
                EquipGun("Ak47");
                break;

            case "Pistol":
                EquipGun("Pistol");
                break;

            case "Sniperka":
                EquipGun("Sniperka");
                break;

            case "Granade":
                EquipGun("Granade");
                break;

            case "Bazooka":
                EquipGun("Bazooka");
                break;

            case "ShotGun":
                EquipGun("ShotGun");
                break;
        }
    }

    protected void EquipGun(string gunTag)
    {
        foreach (MainGun gun in guns)
        {
            if (gun.CompareTag(gunTag))
            {
                gun.gameObject.SetActive(true); // Включаем нужное оружие
                Debug.Log($"Equipped {gunTag}"); // Отладочное сообщение
            }
            else
            {
                gun.gameObject.SetActive(false); // Выключаем все остальные
                Debug.Log($"Unequipped {gun.tag}"); // Отладочное сообщение
            }
        }
    }


    protected void Flip(float moveInput)
    {
        if (moveInput < 0)
        {
            transform.localScale = new Vector3(1, 2.475f, 1);
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(-1, 2.475f, 1);
        }
    }


    public virtual void TakeDamage(int damage)
    {  
    }
}

