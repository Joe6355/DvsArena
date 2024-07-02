using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar:MonoBehaviour
{
    public Image healthBarFill;
    public MainPlayer player; // ������ �� ������

    private void Update()
    {
        // ��������� ���������� ������� �������� � ����������� �� �������� �������� ������
        healthBarFill.fillAmount = (float)player.hp / player.maxHP;
    }
}
