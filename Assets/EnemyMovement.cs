using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public float speed = 10f;
    private int wavepointIndex = 0;
    private Transform target;

    private void Start()
    {
        target = Waypoints.waypoints[wavepointIndex];
    }

    private void Update()
    {
        Vector3 waypointDirection = target.position - transform.position;
        transform.Translate(waypointDirection.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(target.position, transform.position) <= 0.4f)
        {
            if (wavepointIndex < Waypoints.waypoints.Length)
                target = Waypoints.waypoints[wavepointIndex++];
            else Destroy(gameObject);
        }
    }
}
