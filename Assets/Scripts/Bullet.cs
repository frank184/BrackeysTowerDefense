using UnityEngine;

public class Bullet : MonoBehaviour {
    private Transform target;

    [Header("Attributes")]
    public float speed = 70f;

    [Header("Setup")]
    [SerializeField]
    private GameObject hitPrefab;

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
                transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void HitTarget()
    {
        Destroy(Instantiate(hitPrefab, transform.position, transform.rotation), 2f);
        Destroy(target.gameObject);
        Destroy(gameObject);
    }
}
