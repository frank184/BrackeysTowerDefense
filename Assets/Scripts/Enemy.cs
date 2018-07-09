using UnityEngine;

public class Enemy : MonoBehaviour {
    public int scale;

    public float health = 100;
    public int reward = 10;

    [SerializeField]
    private GameObject deathEffectPrefab;

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
        if (deathEffectPrefab != null)
        {
            Vector3 deathEffectPosition = new Vector3(transform.position.x, deathEffectPrefab.transform.position.y, transform.position.z);
            Destroy(Instantiate(deathEffectPrefab, deathEffectPosition, Quaternion.identity), 2);
        }
    }
}
