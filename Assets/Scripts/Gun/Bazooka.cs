using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : MainGun
{
    public int maxShots = 3; // ������������ ���������� ���������
    private int shotCount = 0; // ������� ������� ���������

    public override void Shoot()
    {
        if (shotCount < maxShots)
        {
            base.Shoot(); // ��������� �������
            shotCount++;

            if (shotCount >= maxShots)
            {
                Destroy(gameObject); // ���������� ������ ����� ������������� ���������� ���������
            }
        }
    }
}
