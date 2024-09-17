using UnityEngine;
using System.Collections;

public class Destroer : MonoBehaviour
{
    public int hp = 40; // �������� �����
    public Sprite[] sprites; // ������ �������� ��� ������������
    private SpriteRenderer spriteRenderer; // ��������� SpriteRenderer
    private int currentSpriteIndex = 0; // ������ �������� �������

    [SerializeField] private GameObject effDes;
    private Vector3 originalPosition; // ������������ ������� �������
    protected Animator animator;

    public virtual void Start()
    {
        gameObject.SetActive(true);
        // �������� ��������� SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ��������� ������������ ������� �������
        originalPosition = transform.localPosition;

        // ������������� ��������� ������
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }

        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, ��� ������������ ��������� � �����
        Bullet bull = collision.gameObject.GetComponent<Bullet>();
        if (bull != null) // ���� ������ �������� �����
        {
            TakeDamage(bull.damage); // ��������� �������� � ����������� �� ����� ����
            Destroy(collision.gameObject); // ���������� ���� ����� ������������
            StartCoroutine(Shake(0.1f, 0.05f)); // ��������� �������� �������� ��� ���������
        }
    }

    // ����� ��� ����� �������
    private void ChangeSprite(int hp)
    {
        // ��������� �������� �������� HP � ������ ������ � ����������� �� ����
        if (hp == 30)
        {
            spriteRenderer.sprite = sprites[1]; // ������ �� ������ � �������� 1
        }
        else if (hp == 20)
        {
            spriteRenderer.sprite = sprites[2]; // ������ �� ������ � �������� 2
        }
        else if (hp == 10)
        {
            spriteRenderer.sprite = sprites[3]; // ������ �� ������ � �������� 3
        }
        else if (hp <= 0)
        {
            Instantiate(effDes, transform.position, Quaternion.identity);
            //gameObject.SetActive(false); // ������ �� ������ � �������� 4 � ��������� ������
            Destroy(gameObject);
            StopCoroutine("Shake");
        }
    }

    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
        ChangeSprite(hp);
    }

    // ������� ��� ������� ��������
    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null; // ���� �� ���������� �����
        }

        transform.localPosition = originalPosition; // ���������� ������ �� ������������ �������
    }
}
