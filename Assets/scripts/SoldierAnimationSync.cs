using UnityEngine;

public class SoldierAnimationSync : MonoBehaviour
{
    private Animator anim;
    public Rigidbody playerRigidbody;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerRigidbody != null)
        {
            Vector3 flatVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
            float currentSpeed = flatVelocity.magnitude;

            anim.SetFloat("Speed", currentSpeed);
        }
    }
}