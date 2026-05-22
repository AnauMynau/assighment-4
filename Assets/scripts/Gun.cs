using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect; // Ёффект попадани€/взрыва
    public Camera activeCamera; // —юда прокинь активную камеру

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Ћева€ кнопка мыши
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (muzzleFlash != null) muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(activeCamera.transform.position, activeCamera.transform.forward, out hit, range))
        {
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            // —оздаем эффект в точке попадани€
            if (impactEffect != null)
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
        }
    }
}