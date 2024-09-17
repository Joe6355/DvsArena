using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniperka : MainGun
{
    private bool isReloading = false; // Флаг для проверки, идет ли перезарядка
    private int shotCount = 0; // Счетчик выстрелов до перезарядки

    public int maxShotsBeforeReload = 5; // Максимальное количество выстрелов до перезарядки
    public GameObject objectPrefab; // Префаб магазина
    public Transform spawnPoint; // Точка появления магазина
    protected GameObject spawnedObject; // Переменная для хранения ссылки на созданный объект

    protected override void Start()
    {
        base.Start();
    }

    public override void Shoot()
    {
<<<<<<< HEAD
        base.Shoot(); // Одиночный выстрел
        PlaySound(sounds[0], destroyed: true);
=======
        if (isReloading)
            return; // Не стреляем, если идет перезарядка

        if (Time.time >= lastShootTime + shootCooldown)
        {
            base.Shoot(); // Выполняем основной выстрел из базового класса
            if (anim != null)
            {
                anim.SetTrigger("Shoot"); // Активируем триггер "Shoot" в Animator
                PlaySound(sounds[0], volume: 1f, destroyed: true); // Воспроизводим звук выстрела
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
            SpawnObject();
            Debug.Log("Анимация перезарядки");
            PlaySound(sounds[1], volume: 0.5f, destroyed: true); // Воспроизводим звук перезарядки
        }

        // Ожидание времени перезарядки
        yield return new WaitForSeconds(shootCooldown); // Используем shootCooldown как время перезарядки

        // Спавн магазина
        //SpawnObject();

        isReloading = false; // Снимаем флаг перезарядки
        if (anim != null)
        {
            anim.ResetTrigger("Reload"); // Сбрасываем триггер анимации
            Debug.Log("Сброс триггера");
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

            Destroy(spawnedObject, 2f); // Уничтожаем объект через 2 секунды
        }
>>>>>>> JoeDev
    }
}
