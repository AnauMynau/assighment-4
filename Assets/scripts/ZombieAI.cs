using UnityEngine;
using UnityEngine.AI;
using TMPro; // Подключаем библиотеку TextMeshPro

public class ZombieAI : MonoBehaviour
{
    public float health = 100f;
    public float damageToPlayer = 20f;
    public float attackCooldown = 2f;
    private float lastAttackTime;

    [Header("UI Зомби")]
    public TMP_Text hpText; // Изменили тип на TMP_Text
    public Transform hpCanvas;
    private Camera mainCam;

    private Transform playerTransform;
    private PlayerHealth playerHealth;
    private NavMeshAgent agent;
    private Animator anim;
    private bool isDead = false;

    void Start()
    {
        mainCam = Camera.main;
        UpdateHPUI();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
            playerHealth = playerObj.GetComponent<PlayerHealth>();
        }

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (hpCanvas != null && mainCam != null)
        {
            hpCanvas.LookAt(hpCanvas.position + mainCam.transform.rotation * Vector3.forward, mainCam.transform.rotation * Vector3.up);
        }

        if (isDead || playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance > agent.stoppingDistance)
        {
            agent.SetDestination(playerTransform.position);
            if (anim != null) anim.SetFloat("Speed", agent.velocity.magnitude);
        }
        else
        {
            if (anim != null) anim.SetFloat("Speed", 0);
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        lastAttackTime = Time.time;
        if (anim != null) anim.SetTrigger("Attack");
        if (playerHealth != null) playerHealth.TakeDamage(damageToPlayer);
    }

    public void ApplyDamage(float damage)
    {
        TakeDamage(damage);
    }

    private void TakeDamage(float amount)
    {
        if (isDead) return;

        health -= amount;
        UpdateHPUI();

        if (anim != null && health > 0) anim.SetTrigger("Hit");

        if (health <= 0)
        {
            Die();
        }
    }

    void UpdateHPUI()
    {
        if (hpText != null) hpText.text = health.ToString();
    }

    void Die()
    {
        // Защита, чтобы за одного зомби не давали 2 очка, если пули попали одновременно
        if (isDead) return;

        isDead = true;
        agent.isStopped = true;
        if (anim != null) anim.SetTrigger("Die");

        if (hpCanvas != null) hpCanvas.gameObject.SetActive(false);

        GetComponent<Collider>().enabled = false;

        // ---- ОТПРАВЛЯЕМ 1 ОЧКО В СЧЕТЧИК ----
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(1);
        }
        // -------------------------------------

        Destroy(gameObject, 4f);
    }
}