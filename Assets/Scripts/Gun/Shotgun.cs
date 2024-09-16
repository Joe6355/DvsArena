using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MainGun
{
    public int burstCount = 3; // ���������� ��������� � �������
    public float burstInterval = 0.1f; // �������� ����� ����������
    private bool isReloading = false; // ���� ��� ��������, ���� �� �����������

    public GameObject objectPrefab; // ������ ��������
    public Transform spawnPoint; // ����� ��������� ��������
    protected GameObject spawnedObject; // ���������� ��� �������� ������ �� ��������� ������

    public override void Shoot()
    {
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
            if (anim != null)
            {
                anim.SetTrigger("Shoot");
            }
            Effect();

            for (int j = 0; j < 3; j++) // ��� ����
            {
                float angle = Random.Range(-20f, 20f);
                Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, angle);

                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, rotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                float directionMultiplier = (transform.lossyScale.x > 0) ? 1 : -1;
                Vector2 shootDirection = rotation * Vector2.right * directionMultiplier;

                rb.velocity = shootDirection * projectileSpeed;

                if (directionMultiplier < 0)
                {
                    Vector3 projectileScale = projectile.transform.localScale;
                    projectileScale.x *= -1;
                    projectile.transform.localScale = projectileScale;
                }

                Collider2D projectileCollider = projectile.GetComponent<Collider2D>();
                if (projectileCollider != null)
                {
                    StartCoroutine(DisableColliderTemporarily(projectileCollider, 0.125f));
                }
            }

            if (sounds.Length > 0) // �������� �� ����� �������
            {
                PlaySound(sounds[0], destroyed: true);
            }
            else
            {
                Debug.LogWarning("������ sounds ���� ��� �� �������� ���������.");
            }

            yield return new WaitForSeconds(burstInterval);
        }

        StartReloading();
    }

    private IEnumerator DisableColliderTemporarily(Collider2D collider, float duration)
    {
        collider.enabled = false;
        yield return new WaitForSeconds(duration);
        collider.enabled = true;
    }

    private void StartReloading()
    {
        isReloading = true;
        if (anim != null)
        {
            anim.SetTrigger("Reload");
            Debug.Log("������ �������� �����������");
        }
        PlaySound(sounds[1], destroyed: true);
    }

    public void FinishReloading()
    {
        isReloading = false;
        if (anim != null)
        {
            anim.ResetTrigger("Reload");
        }
        Debug.Log("����������� ���������");
    }

    public void SpawnObject()
    {
        spawnedObject = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);
        spawnedObject.SetActive(true);

        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 1f;
        }

        Destroy(spawnedObject, 2f);
    }
}
