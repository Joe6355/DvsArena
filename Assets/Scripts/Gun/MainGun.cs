using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGun : Sounds
{
    public Animator anim;
    public GameObject projectilePrefab; // ������ �������
    public Transform firePoint; // �����, �� ������� ����� �������� �������
    public float projectileSpeed = 10f; // �������� �������
    public float shootCooldown = 1f; // ����� ����������� ����� ����������

    public GameObject effectShoot;

    protected float lastShootTime; // ����� ���������� ��������

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void Shoot()
    {
        if (Time.time >= lastShootTime + shootCooldown)
        {
            // �������� �������
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            // ���������� ����������� ��������� � ����������� ����������� ����
            float directionMultiplier = (transform.lossyScale.x > 0) ? 1 : -1; // ���������� ����������� �� ��������
            Vector2 shootDirection = firePoint.right * directionMultiplier; // ��������� ����������� ��������
            rb.velocity = shootDirection * projectileSpeed;

            // ������������ ���������� ������� ����
            if (directionMultiplier < 0)
            {
                // �������������� ������ ���� �� ��� X
                Vector3 projectileScale = projectile.transform.localScale;
                projectileScale.x *= -1;
                projectile.transform.localScale = projectileScale;
            }

            lastShootTime = Time.time; // ��������� ����� ���������� ��������
        }
    }

    public virtual void Effect()
    {
        
        Instantiate(effectShoot, firePoint.position, Quaternion.identity);
    }
}

