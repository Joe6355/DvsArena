using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MainGun
{
    public int pelletCount = 3; // Количество дробинок
    public float spreadAngle = 15f; // Угол разброса дробинок

    public override void Shoot()
    {
        if (Time.time >= lastShootTime + shootCooldown)
        {
            for (int i = 0; i < pelletCount; i++)
            {
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                float angle = Random.Range(-spreadAngle, spreadAngle);
                Vector2 shootDirection = Quaternion.Euler(0, 0, angle) * firePoint.right * (firePoint.lossyScale.x > 0 ? 1 : -1);
                rb.velocity = shootDirection * projectileSpeed;
            }
            lastShootTime = Time.time; // Обновляем время последнего выстрела
        }
    }
}
