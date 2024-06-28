using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MainGun
{
    public float arcHeight = 2f; // Высота дуги

    public override void Shoot()
    {
        if (Time.time >= lastShootTime + shootCooldown)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            Vector2 shootDirection = firePoint.right * (firePoint.lossyScale.x > 0 ? 1 : -1);
            float flightTime = 2f * arcHeight / projectileSpeed;
            rb.velocity = new Vector2(shootDirection.x, arcHeight / flightTime);

            lastShootTime = Time.time; // Обновляем время последнего выстрела
        }
    }
}
