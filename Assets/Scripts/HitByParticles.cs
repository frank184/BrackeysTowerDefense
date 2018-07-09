using UnityEngine;

public class HitByParticles : MonoBehaviour {
    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == LayerMask.NameToLayer(Bullet.LAYER))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            Enemy enemy = GetComponent<Enemy>();
            enemy.TakeDamage(bullet.damage);
        }
    }
}
