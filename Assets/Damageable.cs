using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    public bool disableSimulation = false;

    Animator animator;

    Rigidbody2D rb;

    Collider2D physicsCollider;

    bool isAlive = true;
    private float health;

    public float Health
    {
        set
        {
            if (value < health)
            {
                animator.SetTrigger("hit");
            }
            health = value;
            if (health <= 0)
            {
                animator.SetBool("isAlive", false);
            }
        }
        get
        {
            return Health;
        }
    }
    public bool Targetable { get { return Targetable; } }

    bool IDamageable.Targetable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void OnHit(float damage)
    {
        throw new System.NotImplementedException();
    }

    public void OnObjectDestroyed()
    {
        throw new System.NotImplementedException();
    }
}