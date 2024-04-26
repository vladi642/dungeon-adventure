using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public float damage = 1;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public float health;    
    private bool isAlive;
    public detectionzone detectionzone;

    void FixedUpdate()
    {
        Collider2D detectedObject0 = detectionzone.detectedObjs[0];
        if (detectedObject0)
        {
            Vector2 direction = (detectedObject0.transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
    }
    public float Health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
    }
   

    // Update is called once per frame
    void Update()
    {
    }

    public void OnHit(float damage)
    {
        Health -= damage;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        IDamageable damageable = col.collider.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.OnHit(damage);
        }
    }

    void OnCollisionEnter2D(Collision col)
    {
        Debug.Log("Test");
    }
}