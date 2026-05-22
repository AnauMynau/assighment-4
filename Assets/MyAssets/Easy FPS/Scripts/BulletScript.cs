using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 20f;
    public float speed = 100f;
    private Vector3 lastPos;

    void Start()
    {
        lastPos = transform.position;
        Destroy(gameObject, 5f); // Удаляем через 5 сек, если улетела в небо
    }

    void Update()
    {
        // Двигаем пулю вперед
        transform.position += transform.forward * speed * Time.deltaTime;

        // Пускаем луч от позиции в прошлом кадре до текущей
        Vector3 dir = transform.position - lastPos;

        // RaycastAll пробивает всё на своем пути
        RaycastHit[] hits = Physics.RaycastAll(lastPos, dir.normalized, dir.magnitude);

        foreach (RaycastHit hit in hits)
        {
            // Игнорируем самого игрока, камеру и пушку
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Weapon") || hit.collider.CompareTag("MainCamera"))
                continue;

            Debug.Log("Пуля попала в: " + hit.collider.gameObject.name);

            // GetComponentInParent ищет скрипт и на самом объекте, и на его "родителях"
            ZombieAI zombie = hit.collider.GetComponentInParent<ZombieAI>();

            if (zombie != null)
            {
                Debug.Log("ЕСТЬ ПРОБИТИЕ! Урон нанесен.");
                zombie.ApplyDamage(damage);
            }

            // Уничтожаем пулю после попадания во что угодно (кроме игрока)
            Destroy(gameObject);
            return;
        }

        lastPos = transform.position;
    }
}