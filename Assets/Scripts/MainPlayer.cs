using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayer : Sounds
{
    public int maxHP;
    public int hp;
    public float moveSpeed;
    public float jumpForse;
    protected Rigidbody2D rb;
    public bool isGrounded;

    public MainGun [] guns;

    public int totalLives = 3;//общее кол-во жизней
    public Transform respawnPoint;
    public Text livesText;
    public float respawnDelay = 2f;// кд на респ
    //public GameObject shieldObject;
    //public float shieldDuration = 3f;//длительность щита
    public GameObject player;

    public Collider2D headCollDef;
    public Collider2D bodyCollDef;
    public Collider2D headCollSit;
    public Collider2D bodyCollSit;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = maxHP;
        UpdateLivesText();

        headCollSit.gameObject.SetActive(false);
    }

    protected void DefStatus()
    {
        headCollSit.gameObject.SetActive(false);
        headCollDef.gameObject.SetActive(true);
        bodyCollSit.gameObject.SetActive(false);
        bodyCollDef.gameObject.SetActive(true);
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


    protected void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + totalLives;
        }
    }
    protected IEnumerator Respawn()
    {
        Vector3 originalPosition = transform.position;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true; // Отключаем физику

        // Перемещаем игрока за пределы видимости
        transform.position = new Vector3(9999, 9999, 9999);

        yield return new WaitForSeconds(respawnDelay); // Задержка перед возрождением

        // Возвращаем игрока на точку возрождения
        transform.position = respawnPoint.position;
        hp = maxHP;
        rb.isKinematic = false; // Включаем физику
    }





}

