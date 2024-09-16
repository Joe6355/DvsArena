using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ak47 : MainGun
{
    public int burstCount = 5; // ���������� ��������� � �������
    public float burstInterval = 0.1f; // �������� ����� ���������� � �������

    private bool isReloading = false; // ���� ��� ��������, ���� �� �����������

    public GameObject objectPrefab; // ������ ��������
    public Transform spawnPoint; // ����� ��������� ��������
    protected GameObject spawnedObject; // ���������� ��� �������� ������ �� ��������� ������

    public override void Shoot()
    {
        // ���������, ���� �� �����������
        if (isReloading)
            return; // ���� ���� �����������, �������� ���������

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

            // ������������� �������� ��������
            if (anim != null)
            {
                anim.SetTrigger("Shoot");
            }

            PlaySound(sounds[0], destroyed: true);

            yield return new WaitForSeconds(burstInterval);
        }

        // ����� ���������� ������� ��������� �����������
        StartReloading();
    }

    private void StartReloading()
    {
        isReloading = true; // �������� ���� �����������

        if (anim != null)
        {
            anim.SetTrigger("Reload"); // ��������� �������� �����������
            Debug.Log("����������� ��������");
            SpawnObject();
        }

        PlaySound(sounds[1], destroyed: true);
    }

    // ���� ����� ���������� ����� ������� ��������, ����� ����������� ���������
    public void FinishReloading()
    {
        isReloading = false; // ������� ���� �����������
        anim.ResetTrigger("Reload"); // ���������� �������
        Debug.Log("����������� ���������");
    }

    // ���� ����� ���������� ����� ������� �������� ��� ������ ������� (��������, ��������� ��������)
    public void SpawnObject()
    {
        spawnedObject = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);
        spawnedObject.SetActive(true);

        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 1f; // �������� ����������, ����� ������ �����
        }

        Destroy(spawnedObject, 2f); // ���������� ������ ����� 2 �������
    }
}
