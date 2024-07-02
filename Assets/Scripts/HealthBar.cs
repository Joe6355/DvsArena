using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar:MonoBehaviour
{
    public Image healthBarFill;
    public MainPlayer player; // Ссылка на игрока

    private void Update()
    {
        // Обновляем заполнение полоски здоровья в зависимости от текущего здоровья игрока
        healthBarFill.fillAmount = (float)player.hp / player.maxHP;
    }
}
