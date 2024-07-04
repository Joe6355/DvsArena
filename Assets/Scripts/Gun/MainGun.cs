using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGun : Sounds
{
    public GameObject projectilePrefab; // ������ �������
    public Transform firePoint; // �����, �� ������� ����� �������� �������
    public float projectileSpeed = 10f; // �������� �������
    public float shootCooldown = 1f; // ����� ����������� ����� ����������

    protected float lastShootTime; // ����� ���������� ��������

    public virtual void Shoot()
    {
        if (Time.time >= lastShootTime + shootCooldown)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            // ������������� ����������� ������� � ����������� �� ����������� ���������
            Vector2 shootDirection = firePoint.right * (firePoint.lossyScale.x > 0 ? 1 : -1);
            rb.velocity = shootDirection * projectileSpeed;

            lastShootTime = Time.time; // ��������� ����� ���������� ��������
        }
    }
}
