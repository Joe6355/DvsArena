using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnInterval = 5f;
    public GameObject timerCirclePrefab;

    private float timer;
    private SpriteRenderer timerCircleRenderer;
    private GameObject timerCircle; // ѕеремещаем объ€вление сюда

    void Start()
    {
        timer = spawnInterval;

        timerCircle = Instantiate(timerCirclePrefab, transform.position + Vector3.up * 2, Quaternion.identity, transform);
        timerCircle.transform.localScale = new Vector3(timerCircle.transform.localScale.x, 6f, timerCircle.transform.localScale.z);
        timerCircleRenderer = timerCircle.GetComponent<SpriteRenderer>();
        timerCircle.SetActive(false);

     
        

    }




    void Update()
    {
        if (!IsObjectAtSpawner())
        {
            timer -= Time.deltaTime;
            if (timerCircleRenderer != null)
            {
                float alpha = Mathf.Lerp(1f, 0f, timer / spawnInterval);
                Color currentColor = timerCircleRenderer.color;
                timerCircleRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                timerCircle.SetActive(true);
            }
            if (timer <= 0f)
            {
                SpawnObject();
                timer = spawnInterval;
                if (timerCircleRenderer != null)
                {
                    timerCircle.SetActive(false);
                }
            }
        }
    }


    private void SpawnObject()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }

    private bool IsObjectAtSpawner()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(prefab.tag))
            {
                return true;
            }
        }
        return false;
    }

}
