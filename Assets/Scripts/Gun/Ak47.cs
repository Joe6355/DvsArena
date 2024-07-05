using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ak47 : MainGun
{
    public int burstCount = 5; //  оличество выстрелов в очереди
    public float burstInterval = 0.1f; // »нтервал между выстрелами в очереди

    public override void Shoot()
    {

        if (Time.time >= lastShootTime + shootCooldown)
        {
            StartCoroutine(BurstFire());
            lastShootTime = Time.time; // ќбновл€ем врем€ последнего выстрела
        }
    }

    private IEnumerator BurstFire()
    {
        for (int i = 0; i < burstCount; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            // ”станавливаем направление снар€да в зависимости от направлени€ персонажа
            Vector2 shootDirection = firePoint.right * (firePoint.lossyScale.x > 0 ? 1 : -1);
            rb.velocity = shootDirection * projectileSpeed;

            yield return new WaitForSeconds(burstInterval);
        }
    }
}
