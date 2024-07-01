using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet:MonoBehaviour
{
    public int damage = 10; // ”рон пули
    public float lifetime = 2f; // ¬рем€ жизни пули

    private void Start()
    {
        Destroy(gameObject, lifetime); // ”ничтожаем пулю по истечении времени жизни
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        Destroy(gameObject); // ”ничтожаем пулю при столкновении с любым объектом
    }
}
