using UnityEngine;

public class Enemy : MonoBehaviour {
    public int scale;

    public float health = 100;
    public int reward = 10;

    private void Start()
    {
        transform.localScale = Vector3.one * scale;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        Player.Money += reward;
    }
}
