using UnityEngine;

public class HitByParticles : MonoBehaviour {
    public float damage = 5f;
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Bullets")
        {
            Enemy enemy = GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
    }
}
