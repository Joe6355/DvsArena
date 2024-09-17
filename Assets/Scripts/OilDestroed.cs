using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilDestroed : Destroer
{
    public float explosionRadius = 5f; // Радиус взрыва
    public int explosionDamage = 20;   // Урон взрыва

    public override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Explosion();
        }
    }

    public void Explosion()
    {
        // Запускаем анимацию взрыва
        animator.SetTrigger("Explosion");

        // Проверяем, кто попал в радиус взрыва
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        Debug.Log("Objects in explosion radius: " + hitObjects.Length);

        foreach (Collider2D obj in hitObjects)
        {
            // Проверяем наличие компонента MainPlayer или его потомков
            MainPlayer player = obj.GetComponent<MainPlayer>();
            if (player != null)
            {
                Debug.Log("Player found: " + obj.name + ", applying damage");
                // Наносим урон игроку через его существующий метод TakeDamage
                player.TakeDamage(explosionDamage);
            }
            else
            {
                Debug.Log("No MainPlayer component found on " + obj.name);
            }
        }

        // Уничтожаем бочку через 1 секунду после взрыва
        Destroy(gameObject, 1f);
    }

    // Визуализация радиуса взрыва в редакторе
    private void OnDrawGizmosSelected()
    {
        // Цвет Gizmo
        Gizmos.color = Color.red;

        // Рисуем окружность, обозначающую радиус взрыва
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}


