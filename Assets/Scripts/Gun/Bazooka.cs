using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : MainGun
{
    public int maxShots = 3; // Максимальное количество выстрелов
    private int shotCount = 0; // Текущий счетчик выстрелов

    public override void Shoot()
    {
        if (shotCount < maxShots)
        {
            base.Shoot(); // Одиночный выстрел
            shotCount++;

            if (shotCount >= maxShots)
            {
                Destroy(gameObject); // Уничтожаем оружие после максимального количества выстрелов
            }
        }
    }
}
