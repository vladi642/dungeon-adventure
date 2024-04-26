using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour , IDamageable
{
    public float Health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    float IDamageable.Health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    bool IDamageable.Targetable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    float IDamageable.health { set => throw new System.NotImplementedException(); }

    public float health = 3f;
    public bool targetable = true;
    public void OnHit(float damage)
    {
        throw new System.NotImplementedException();
    }

    void IDamageable.OnHit(float damage)
    {
        Health -= damage;
    }

    void IDamageable.OnObjectDestroyed()
    {
        throw new System.NotImplementedException();
    }

    public void OnObjectDestroyed()
    {
        Destroy(gameObject);
    }

}
