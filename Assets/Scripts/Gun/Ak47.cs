using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ak47 : MainGun
{
    public int burstCount = 5; // Количество выстрелов в очереди
    public float burstInterval = 0.1f; // Интервал между выстрелами в очереди

    private bool isReloading = false; // Флаг для проверки, идет ли перезарядка

    public GameObject objectPrefab; // Префаб магазина
    public Transform spawnPoint; // Точка появления магазина
    protected GameObject spawnedObject; // Переменная для хранения ссылки на созданный объект

    public override void Shoot()
    {
        // Проверяем, идет ли перезарядка
        if (isReloading)
            return; // Если идет перезарядка, выстрелы запрещены

        if (Time.time >= lastShootTime + shootCooldown)
        {
            StartCoroutine(BurstFire());
            lastShootTime = Time.time; // Обновляем время последнего выстрела
        }
    }

    private IEnumerator BurstFire()
    {
        for (int i = 0; i < burstCount; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            // Определяем направление персонажа и настраиваем направление пули
            float directionMultiplier = (transform.lossyScale.x > 0) ? 1 : -1; // Определяем направление по масштабу
            Vector2 shootDirection = firePoint.right * directionMultiplier; // Вычисляем направление выстрела
            rb.velocity = shootDirection * projectileSpeed;

            // Корректируем ориентацию спрайта пули
            if (directionMultiplier < 0)
            {
                // Переворачиваем спрайт пули по оси X
                Vector3 projectileScale = projectile.transform.localScale;
                projectileScale.x *= -1;
                projectile.transform.localScale = projectileScale;
            }

            // Воспроизводим анимацию выстрела
            if (anim != null)
            {
                anim.SetTrigger("Shoot");
            }

            PlaySound(sounds[0], destroyed: true);

            yield return new WaitForSeconds(burstInterval);
        }

        // После завершения очереди запускаем перезарядку
        StartReloading();
    }

    private void StartReloading()
    {
        isReloading = true; // Включаем флаг перезарядки

        if (anim != null)
        {
            anim.SetTrigger("Reload"); // Запускаем анимацию перезарядки
            Debug.Log("Перезарядка началась");
            SpawnObject();
        }

        PlaySound(sounds[1], destroyed: true);
    }

    // Этот метод вызывается через событие анимации, когда перезарядка завершена
    public void FinishReloading()
    {
        isReloading = false; // Снимаем флаг перезарядки
        anim.ResetTrigger("Reload"); // Сбрасываем триггер
        Debug.Log("Перезарядка завершена");
    }

    // Этот метод вызывается через событие анимации для спавна объекта (например, падающего магазина)
    public void SpawnObject()
    {
        spawnedObject = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);
        spawnedObject.SetActive(true);

        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 1f; // Включаем гравитацию, чтобы объект падал
        }

        Destroy(spawnedObject, 2f); // Уничтожаем объект через 2 секунды
    }
}
