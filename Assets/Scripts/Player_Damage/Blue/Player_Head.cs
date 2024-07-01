using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Head : MonoBehaviour
{
    public MainPlayer playerBlue;
    public Bullet bullet;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            int damage = bullet.damage * 3; // Пример удвоенного урона для головы
            playerBlue.TakeDamage(damage);
            Debug.Log("В голову");
            Destroy(collision.gameObject); // Уничтожаем пулю после попадания
        }
    }
}
