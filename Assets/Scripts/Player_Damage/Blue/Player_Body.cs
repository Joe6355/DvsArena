using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Body : MonoBehaviour
{
    public MainPlayer playerBlue;
    public Bullet bullet;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            int damage = bullet.damage;
            playerBlue.TakeDamage(damage);
            Debug.Log("В тело");
            Destroy(collision.gameObject); // Уничтожаем пулю после попадания
        }
    }
}
