using UnityEngine;

public class BulletProjectile : Bullet {
    private Transform target;

    [Header("Attributes")]
    public float speed = 70f;
    public float explosionRadius = 0f;
    public float reduceMovementSpeed = 0f;
    public float reduceMovementTime = 0f;

    [Header("Setup")]

    [SerializeField]
    private GameObject hitPrefab;

    private Vector3 targetLastKnown;

    public void Seek(Transform target)
    {
        this.target = target;
        targetLastKnown = target.position;
    }

    private void Update()
    {
        Debug.Log(target);
        if (target != null)
        {
            targetLastKnown = target.position;
            MoveTowards(target.position);
        }
        else
            MoveTowards(targetLastKnown);
    }

    void MoveTowards(Vector3 position)
    {
        Vector3 dir = position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
            HitTarget();
        else
        {
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            transform.LookAt(position);
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
            if (child.tag == ENEMY_TAG)
            {
                if (reduceMovementSpeed != 0 && reduceMovementTime != 0)
                {
                    EnemyMovement move = child.gameObject.GetComponent<EnemyMovement>();
                    move.SlowMovement(reduceMovementSpeed, reduceMovementTime);
                }
                Damage(child.transform);
            }
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
