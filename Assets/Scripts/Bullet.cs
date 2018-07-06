using UnityEngine;
using System;
using System.Collections;

public class Bullet : MonoBehaviour {
    private Transform target;

    [Header("Attributes")]
    public float damage = 10f;
    public float speed = 70f;
    public float explosionRadius = 0f;
    public float reduceMovementSpeed = 0f;
    public float reduceMovementTime = 0f;

    [Header("Setup")]
    
    [SerializeField]
    private GameObject hitPrefab;

    public string enemyTag = "Enemy";

    public void Seek(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
                HitTarget();
            else
            {
                transform.Translate(dir.normalized * distanceThisFrame, Space.World);
                transform.LookAt(target);
            }
        }
        else
        {
            Dispose();
        }
    }

    void HitTarget()
    {
        if (hitPrefab != null)
        {
            GameObject hitEffect = Instantiate(hitPrefab, target.position, target.rotation);
            Destroy(hitEffect, 2f);
        }
        if (explosionRadius > 0f)
            Explode();
        else
            Damage(target);
        Dispose();
    }

    void Explode()
    {
        Collider[] hitByAOE = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider child in hitByAOE)
            if (child.tag == enemyTag)
            {
                if (reduceMovementSpeed != 0 && reduceMovementTime != 0)
                {
                    EnemyMovement move = child.gameObject.GetComponent<EnemyMovement>();
                    StartCoroutine(Slow(move));
                }
                Damage(child.transform);
            }
    }

    // Y U NO WORK
    IEnumerator Slow(EnemyMovement move)
    {
        float startSpeed = move.speed;
        Debug.Log("start enemy move speed " + startSpeed);
        move.speed /= reduceMovementSpeed;
        Debug.Log("new enemy move speed " + move.speed);
        yield return new WaitForSeconds(1);
        move.speed = startSpeed;
        Debug.Log("reset enemy move speed " + move.speed);
    }

    void Damage(Transform transform)
    {
        Enemy enemy = transform.GetComponent<Enemy>();
        enemy.TakeDamage(damage);
    }

    void Dispose()
    {
        Transform child = transform.GetChild(0);
        if (child.name == "DONTDESTROY")
        {
            child.SetParent(transform.parent);
            Destroy(child.gameObject, 10f);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
