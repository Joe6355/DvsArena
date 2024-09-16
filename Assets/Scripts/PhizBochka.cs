using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhizBochka : Destroer
{
    public GameObject[] bochkaKyski; // ������ �������� ��� ������ �����
    public float explosionForce = 300f; // ���� ������� ������
    public float explosionRadius = 2f; // ������ ������� ������

    public override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            // ������� ����� �����
            Explode();
            // ���������� �����
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        // ������� ������ ����� �� ������� � ��� �� �������, ��� ���� �����
        foreach (GameObject kysk in bochkaKyski)
        {
            GameObject spawnedKysk = Instantiate(kysk, transform.position, Quaternion.identity);
            Rigidbody2D rbKysk = spawnedKysk.GetComponent<Rigidbody2D>();

            if (rbKysk != null)
            {
                // ������ ��������� ����������� ��� ������� ������
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                rbKysk.AddForce(randomDirection * explosionForce);
            }

            // ������� ����� ����� 3 �������
            Destroy(spawnedKysk, 10f);
        }
    }
}
