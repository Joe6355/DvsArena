using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MainGun
{
    private bool isReloading = false; // Флаг для проверки, идет ли перезарядка
    private int shotCount = 0; // Счетчик выстрелов до перезарядки

    public int maxShotsBeforeReload = 10; // Максимальное количество выстрелов до перезарядки
    public GameObject objectPrefab; // Префаб магазина
    public Transform spawnPoint; // Точка появления магазина
    protected GameObject spawnedObject; // Переменная для хранения ссылки на созданный объект

    protected override void Start()
    {
        base.Start();
    }

    public override void Shoot()
    {
        if (isReloading)
            return; // Не стреляем, если идет перезарядка

        if (Time.time >= lastShootTime + shootCooldown)
        {
            base.Shoot();
            if (anim != null)
            {
                anim.SetTrigger("Shoot"); // Активируем триггер "Shoot" в Animator

                PlaySound(sounds[0], volume: 0.5f, destroyed: true);
            }
            lastShootTime = Time.time; // Обновляем время последнего выстрела

            shotCount++; // Увеличиваем счетчик выстрелов

            // Запуск перезарядки после достижения максимального количества выстрелов
            if (shotCount >= maxShotsBeforeReload)
            {
                StartCoroutine(ReloadCoroutine());
                shotCount = 0; // Сбрасываем счетчик выстрелов
            }
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true; // Включаем флаг перезарядки
        if (anim != null)
        {
            anim.SetTrigger("Reload"); // Запускаем анимацию перезарядки
            Debug.Log("анимация перезарядки");
            PlaySound(sounds[1], destroyed: true);
        }

        // Ожидание времени перезарядки
        yield return new WaitForSeconds(shootCooldown); // Используем shootCooldown как время перезарядки

        // Спавн магазина
        SpawnObject();

        isReloading = false; // Снимаем флаг перезарядки
        if (anim != null)
        {
            anim.ResetTrigger("Reload"); // Сбрасываем триггер анимации
            Debug.Log("сброс тригера");
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
                rb.gravityScale = 1f; // Включаем гравитацию
            }

            Destroy(spawnedObject, 2f); // Уничтожаем объект через 5 секунд
        }
    }
}