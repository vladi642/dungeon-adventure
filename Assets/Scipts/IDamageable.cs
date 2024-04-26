public interface IDamageable
{
    public float Health { set; get; }
    float health { set; }
    public bool Targetable { set; get; }
    public void OnHit(float damage);
    public void OnObjectDestroyed();
}