using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minDistance = 0.5f; // Минимальное расстояние до головы
    public float smooth = 10f; // Плавность наезда камеры
    public float wallOffset = 0.2f; // Зазор, чтобы камера не терлась прямо о текстуру стены

    private Vector3 defaultLocalPos;
    private Vector3 directionNormalized;
    private float maxDistance;

    void Start()
    {
        // Запоминаем ту идеальную позицию (например X=0.7, Y=0.2, Z=-2.5), которую ты настроил
        defaultLocalPos = transform.localPosition;
        directionNormalized = defaultLocalPos.normalized;
        maxDistance = defaultLocalPos.magnitude;
    }

    void LateUpdate()
    {
        // Позиция головы (родителя)
        Vector3 parentPos = transform.parent.position;
        // Куда камера хочет встать
        Vector3 desiredPos = transform.parent.TransformPoint(defaultLocalPos);

        float currentDistance = maxDistance;

        // Пускаем луч от головы к камере, чтобы найти стены
        RaycastHit[] hits = Physics.RaycastAll(parentPos, desiredPos - parentPos, maxDistance);

        foreach (RaycastHit hit in hits)
        {
            // Игнорируем самого игрока, оружие, врагов и невидимые триггеры
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Weapon") || hit.collider.CompareTag("Enemy") || hit.collider.isTrigger)
                continue;

            // Если нашли стену, считаем дистанцию до нее (минус небольшой зазор)
            float hitDistance = hit.distance - wallOffset;
            if (hitDistance < currentDistance)
            {
                currentDistance = hitDistance;
            }
        }

        // Ограничиваем, чтобы камера не залетела прямо в череп солдату
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // Плавно двигаем камеру на новую позицию
        Vector3 newLocalPos = directionNormalized * currentDistance;
        transform.localPosition = Vector3.Lerp(transform.localPosition, newLocalPos, Time.deltaTime * smooth);
    }
}