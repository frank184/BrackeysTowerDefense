using UnityEngine;

public class BulletLaser : Bullet {
    [SerializeField]
    private GameObject hitPrefab;
    private LineRenderer lineRenderer;
    private Transform target;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (target != null) SetLaser();
        else ResetLaser();
    }

    public void Beam(Transform target)
    {
        this.target = target;
        if (hitPrefab != null)
        {
            GameObject hitEffect = Instantiate(hitPrefab, target.position, Quaternion.identity);
            Destroy(hitEffect, 2f);
        }
    }

    void SetLaser()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, new Vector3(0, 0, distance));
    }

    void ResetLaser()
    {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }
}
