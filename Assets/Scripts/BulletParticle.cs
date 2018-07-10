public class BulletParticle : Bullet {
    public void Spawn()
    {
        Destroy(gameObject, 0.5f);
    }
}