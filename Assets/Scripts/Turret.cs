using UnityEngine;

public class Turret : MonoBehaviour {
    private Transform target;

    [Header("Attributes")]
    public float retargetTimer = 0.5f;
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Setup")]
    [SerializeField]
    private GameObject muzzleFlashPrefab;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Transform pivot;
    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, retargetTimer);
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - pivot.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Quaternion smoothRotation = Quaternion.Lerp(pivot.rotation, lookRotation, Time.deltaTime * turnSpeed);
            Vector3 rotation = smoothRotation.eulerAngles;
            pivot.rotation = Quaternion.Euler(rotation);
            if (fireCountdown <= 0f)
            {
                fireCountdown = 1f / fireRate;
                Shoot();
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (bullet != null) bullet.Seek(target);
            if (muzzleFlashPrefab != null)
            {
                GameObject muzzleFlashObj = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation, firePoint);
                Destroy(muzzleFlashObj, 10f);
            }
        }
        else
        {
            RaycastHit hit;
            bool hitSomething = Physics.Raycast(firePoint.position, firePoint.forward, out hit, range);
            if (hitSomething && hit.collider.tag == enemyTag)
            {
                if (muzzleFlashPrefab != null)
                {
                    GameObject muzzleFlashObj = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation, firePoint);
                    Destroy(muzzleFlashObj, 1f);
                }
            }
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
            target = nearestEnemy.transform;
        else target = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
