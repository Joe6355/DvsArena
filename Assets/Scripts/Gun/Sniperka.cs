using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniperka : MainGun
{
    private bool isReloading = false; // ���� ��� ��������, ���� �� �����������
    private int shotCount = 0; // ������� ��������� �� �����������

    public int maxShotsBeforeReload = 5; // ������������ ���������� ��������� �� �����������
    public GameObject objectPrefab; // ������ ��������
    public Transform spawnPoint; // ����� ��������� ��������
    protected GameObject spawnedObject; // ���������� ��� �������� ������ �� ��������� ������

    protected override void Start()
    {
        base.Start();
    }

    public override void Shoot()
    {
<<<<<<< HEAD
        base.Shoot(); // ��������� �������
        PlaySound(sounds[0], destroyed: true);
=======
        if (isReloading)
            return; // �� ��������, ���� ���� �����������

        if (Time.time >= lastShootTime + shootCooldown)
        {
            base.Shoot(); // ��������� �������� ������� �� �������� ������
            if (anim != null)
            {
                anim.SetTrigger("Shoot"); // ���������� ������� "Shoot" � Animator
                PlaySound(sounds[0], volume: 1f, destroyed: true); // ������������� ���� ��������
            }

            lastShootTime = Time.time; // ��������� ����� ���������� ��������
            shotCount++; // ����������� ������� ���������

            // ������ ����������� ����� ���������� ������������� ���������� ���������
            if (shotCount >= maxShotsBeforeReload)
            {
                StartCoroutine(ReloadCoroutine());
                shotCount = 0; // ���������� ������� ���������
            }
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true; // �������� ���� �����������
        if (anim != null)
        {
            anim.SetTrigger("Reload"); // ��������� �������� �����������
            SpawnObject();
            Debug.Log("�������� �����������");
            PlaySound(sounds[1], volume: 0.5f, destroyed: true); // ������������� ���� �����������
        }

        // �������� ������� �����������
        yield return new WaitForSeconds(shootCooldown); // ���������� shootCooldown ��� ����� �����������

        // ����� ��������
        //SpawnObject();

        isReloading = false; // ������� ���� �����������
        if (anim != null)
        {
            anim.ResetTrigger("Reload"); // ���������� ������� ��������
            Debug.Log("����� ��������");
        }
    }

    public void SpawnObject()
    {
        if (objectPrefab != null && spawnPoint != null)
        {
            spawnedObject = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);
            spawnedObject.SetActive(true);

            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 1f; // �������� ����������
            }

            Destroy(spawnedObject, 2f); // ���������� ������ ����� 2 �������
        }
>>>>>>> JoeDev
    }
}
