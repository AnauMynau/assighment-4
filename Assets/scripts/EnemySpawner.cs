using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Кого спавнить")]
    public GameObject zombiePrefab;

    [Header("Где спавнить")]
    public Transform[] spawnPoints;

    [Header("Как часто (в секундах)")]
    public float spawnInterval = 4f;

    void Start()
    {
        // Запускаем бесконечный цикл появления врагов
        InvokeRepeating(nameof(SpawnZombie), 2f, spawnInterval);
    }

    void SpawnZombie()
    {
        if (spawnPoints.Length == 0 || zombiePrefab == null) return;

        // Выбираем случайную точку из нашего списка
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform selectedPoint = spawnPoints[randomIndex];

        // Создаем зомби
        Instantiate(zombiePrefab, selectedPoint.position, selectedPoint.rotation);
    }
}