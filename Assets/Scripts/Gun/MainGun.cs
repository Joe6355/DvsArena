using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGun : Sounds
{
    public Animator anim;
    public GameObject projectilePrefab; // Префаб снаряда
    public Transform firePoint; // Точка, из которой будут вылетать снаряды
    public float projectileSpeed = 10f; // Скорость снаряда
    public float shootCooldown = 1f; // Время перезарядки между выстрелами

    public GameObject effectShoot;

    protected float lastShootTime; // Время последнего выстрела

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void Shoot()
    {
        if (Time.time >= lastShootTime + shootCooldown)
        {
            // Создание снаряда
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            // Определяем направление персонажа и настраиваем направление пули
            float directionMultiplier = (transform.lossyScale.x > 0) ? 1 : -1; // Определяем направление по масштабу
            Vector2 shootDirection = firePoint.right * directionMultiplier; // Вычисляем направление выстрела
            rb.velocity = shootDirection * projectileSpeed;

            // Корректируем ориентацию спрайта пули
            if (directionMultiplier < 0)
            {
                // Переворачиваем спрайт пули по оси X
                Vector3 projectileScale = projectile.transform.localScale;
                projectileScale.x *= -1;
                projectile.transform.localScale = projectileScale;
            }

            lastShootTime = Time.time; // Обновляем время последнего выстрела
        }
    }

    public virtual void Effect()
    {
        
        Instantiate(effectShoot, firePoint.position, Quaternion.identity);
    }
}

