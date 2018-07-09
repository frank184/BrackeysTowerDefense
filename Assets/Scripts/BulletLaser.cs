using UnityEngine;

public class BulletLaser : Bullet {
    private Transform target;
    private LineRenderer lineRenderer;

    public void Beam(Transform target)
    {
        this.target = target;
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (target != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, target.position);

            Enemy enemy = target.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
