﻿using UnityEngine;

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

    public GameObject bulletPrefab;

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
            LockOn();
            if (fireCountdown <= 0f)
            {
                fireCountdown = 1f / fireRate;
                Shoot();
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    void LockOn()
    {
        Vector3 direction = target.position - pivot.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Quaternion smoothRotation = Quaternion.Lerp(pivot.rotation, lookRotation, Time.deltaTime * turnSpeed);
        Vector3 rotation = smoothRotation.eulerAngles;
        pivot.rotation = Quaternion.Euler(rotation);
    }

    void Shoot()
    {
        // Instantiate Obj from Prefab
        Bullet bullet = bulletPrefab.GetComponent<Bullet>();

        // Bullet Flow
        if (bullet.GetType() == typeof(BulletProjectile))
        {
            GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletProjectile bulletProjectile = bulletObj.GetComponent<BulletProjectile>();
            bulletProjectile.Seek(target);
        }
        else if (bullet.GetType() == typeof(BulletLaser))
        {
            Debug.Log("Did Hit");
            BulletLaser bulletLaser = (BulletLaser)bulletPrefab.GetComponent<Bullet>();
            bulletLaser.Beam(target);
            Enemy enemy = target.GetComponent<Enemy>();
            enemy.TakeDamage(bulletLaser.damage);
        }
        else if (bullet.GetType() == typeof(BulletParticle))
        {
            GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, firePoint);
            BulletParticle bulletParticle = bulletObj.GetComponent<BulletParticle>();
            bulletParticle.Spawn();
        }

        // If muzzle flash
        if (muzzleFlashPrefab != null)
            Destroy(Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation, firePoint), 5);
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
