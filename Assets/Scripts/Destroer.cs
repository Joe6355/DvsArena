using UnityEngine;
using System.Collections;

public class Destroer : MonoBehaviour
{
    public int hp = 40; // Здоровье бочки
    public Sprite[] sprites; // Массив спрайтов для переключения
    private SpriteRenderer spriteRenderer; // Компонент SpriteRenderer
    private int currentSpriteIndex = 0; // Индекс текущего спрайта

    [SerializeField] private GameObject effDes;
    private Vector3 originalPosition; // Оригинальная позиция объекта
    protected Animator animator;

    public virtual void Start()
    {
        gameObject.SetActive(true);
        // Получаем компонент SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Сохраняем оригинальную позицию объекта
        originalPosition = transform.localPosition;

        // Устанавливаем начальный спрайт
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }

        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, что столкновение произошло с пулей
        Bullet bull = collision.gameObject.GetComponent<Bullet>();
        if (bull != null) // Если объект является пулей
        {
            TakeDamage(bull.damage); // Уменьшаем здоровье в зависимости от урона пули
            Destroy(collision.gameObject); // Уничтожаем пулю после столкновения
            StartCoroutine(Shake(0.1f, 0.05f)); // Запускаем корутину дрожания при попадании
        }
    }

    // Метод для смены спрайта
    private void ChangeSprite(int hp)
    {
        // Проверяем значение текущего HP и меняем спрайт в зависимости от него
        if (hp == 30)
        {
            spriteRenderer.sprite = sprites[1]; // Меняем на спрайт с индексом 1
        }
        else if (hp == 20)
        {
            spriteRenderer.sprite = sprites[2]; // Меняем на спрайт с индексом 2
        }
        else if (hp == 10)
        {
            spriteRenderer.sprite = sprites[3]; // Меняем на спрайт с индексом 3
        }
        else if (hp <= 0)
        {
            Instantiate(effDes, transform.position, Quaternion.identity);
            //gameObject.SetActive(false); // Меняем на спрайт с индексом 4 и отключаем объект
            Destroy(gameObject);
            StopCoroutine("Shake");
        }
    }

    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
        ChangeSprite(hp);
    }

    // Корутин для эффекта дрожания
    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null; // ждем до следующего кадра
        }

        transform.localPosition = originalPosition; // возвращаем объект на оригинальную позицию
    }
}
