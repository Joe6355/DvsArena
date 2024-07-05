using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ak47 : MainGun
{
    public int burstCount = 5; // ���������� ��������� � �������
    public float burstInterval = 0.1f; // �������� ����� ���������� � �������

    public override void Shoot()
    {

        if (Time.time >= lastShootTime + shootCooldown)
        {
            StartCoroutine(BurstFire());
            lastShootTime = Time.time; // ��������� ����� ���������� ��������
        }
    }

    private IEnumerator BurstFire()
    {
        for (int i = 0; i < burstCount; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            // ������������� ����������� ������� � ����������� �� ����������� ���������
            Vector2 shootDirection = firePoint.right * (firePoint.lossyScale.x > 0 ? 1 : -1);
            rb.velocity = shootDirection * projectileSpeed;

            yield return new WaitForSeconds(burstInterval);
        }
    }
}
