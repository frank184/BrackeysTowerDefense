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
            pivot.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            //pivot.transform.LookAt(target);
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject obj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = obj.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seek(target);
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
        //if (nearestEnemy != null && shortestDistance <= range && target == null)
        if (nearestEnemy != null && shortestDistance <= range)
            target = nearestEnemy.transform;
        else target = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
