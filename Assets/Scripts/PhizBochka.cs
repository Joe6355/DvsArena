using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhizBochka : Destroer
{
    public GameObject[] bochkaKyski; // Массив объектов для кусков бочки
    public float explosionForce = 300f; // Сила разлета кусков
    public float explosionRadius = 2f; // Радиус разлета кусков

    public override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            // Спавним куски бочки
            Explode();
            // Уничтожаем бочку
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        // Спавним каждый кусок из массива в той же позиции, где была бочка
        foreach (GameObject kysk in bochkaKyski)
        {
            GameObject spawnedKysk = Instantiate(kysk, transform.position, Quaternion.identity);
            Rigidbody2D rbKysk = spawnedKysk.GetComponent<Rigidbody2D>();

            if (rbKysk != null)
            {
                // Задаем случайное направление для разлета кусков
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                rbKysk.AddForce(randomDirection * explosionForce);
            }

            // Удаляем куски через 3 секунды
            Destroy(spawnedKysk, 10f);
        }
    }
}
