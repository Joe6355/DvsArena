using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet:MonoBehaviour
{
    public float lifetime = 2f; // ����� ����� �������

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ������ ��� ������������ � ��������
        Destroy(gameObject);
    }
}
