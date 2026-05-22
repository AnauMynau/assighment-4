using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public GameObject deathExplosion; // Ёффект уничтожени€ врага
    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(player.position);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // ”величение счета при уничтожении
        ScoreManager.instance.AddScore(10);

        // Ёффект взрыва
        if (deathExplosion != null)
        {
            Instantiate(deathExplosion, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}