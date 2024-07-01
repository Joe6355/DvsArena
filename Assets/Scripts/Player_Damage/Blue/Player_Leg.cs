using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Leg : MonoBehaviour
{
    public MainPlayer playerBlue;
    public Bullet bullet;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            int damage = bullet.damage - 1; // ������ ������������ ����� ��� ���
            playerBlue.TakeDamage(damage);
            Debug.Log("� ����");
            Destroy(collision.gameObject); // ���������� ���� ����� ���������
        }
    }
}
