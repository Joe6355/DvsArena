using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilDestroed : Destroer
{
    public float explosionRadius = 5f; // ������ ������
    public int explosionDamage = 20;   // ���� ������

    public override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Explosion();
        }
    }

    public void Explosion()
    {
        // ��������� �������� ������
        animator.SetTrigger("Explosion");

        // ���������, ��� ����� � ������ ������
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        Debug.Log("Objects in explosion radius: " + hitObjects.Length);

        foreach (Collider2D obj in hitObjects)
        {
            // ��������� ������� ���������� MainPlayer ��� ��� ��������
            MainPlayer player = obj.GetComponent<MainPlayer>();
            if (player != null)
            {
                Debug.Log("Player found: " + obj.name + ", applying damage");
                // ������� ���� ������ ����� ��� ������������ ����� TakeDamage
                player.TakeDamage(explosionDamage);
            }
            else
            {
                Debug.Log("No MainPlayer component found on " + obj.name);
            }
        }

        // ���������� ����� ����� 1 ������� ����� ������
        Destroy(gameObject, 1f);
    }

    // ������������ ������� ������ � ���������
    private void OnDrawGizmosSelected()
    {
        // ���� Gizmo
        Gizmos.color = Color.red;

        // ������ ����������, ������������ ������ ������
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}


