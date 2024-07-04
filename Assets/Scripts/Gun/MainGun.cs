using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGun : Sounds
{
    public GameObject projectilePrefab; // Префаб снаряда
    public Transform firePoint; // Точка, из которой будут вылетать снаряды
    public float projectileSpeed = 10f; // Скорость снаряда
    public float shootCooldown = 1f; // Время перезарядки между выстрелами

    protected float lastShootTime; // Время последнего выстрела

    public virtual void Shoot()
    {
        if (Time.time >= lastShootTime + shootCooldown)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            // Устанавливаем направление снаряда в зависимости от направления персонажа
            Vector2 shootDirection = firePoint.right * (firePoint.lossyScale.x > 0 ? 1 : -1);
            rb.velocity = shootDirection * projectileSpeed;

            lastShootTime = Time.time; // Обновляем время последнего выстрела
        }
    }
}
