using System.Collections;
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
        // Rotation
        Vector3 direction = target.position - transform.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Quaternion smoothRotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        Vector3 rotation = smoothRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotation);

        // Direction
        Vector3 waypointDirection = target.position - transform.position;
        transform.Translate(waypointDirection.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(target.position, transform.position) <= 0.4f)
        {
            if (wavepointIndex < Waypoints.waypoints.Length)
                target = Waypoints.waypoints[wavepointIndex++];
            else EndPath();
        }
    }

    public void SlowMovement(float reduceMovementSpeed, float reduceMovementTime)
    {
        StartCoroutine(Slow(reduceMovementSpeed, reduceMovementTime));
    }

    IEnumerator Slow(float reduceMovementSpeed, float reduceMovementTime)
    {
        float startSpeed = speed;
        speed /= reduceMovementSpeed;
        yield return new WaitForSeconds(reduceMovementTime);
        speed = startSpeed;
        Debug.Log("reset enemy move speed " + speed);
    }

    void EndPath()
    {
        Player.Lives -= 1;
        Destroy(gameObject);
    }
}
