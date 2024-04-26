using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeController : MonoBehaviour
{
    public Animator animator;

    public Transform player; // Променлива за играча

    public float moveSpeed = 5f; // Скорост на движение на слима

    private Rigidbody2D rb; // Rigidbody компонент за слима

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаване на Rigidbody компонент
    }

    private void Update()
    {
        if (player != null) // Ако играчът съществува
        {
            // Изчисляване на посоката към играча
            Vector2 moveDirection = (player.position - transform.position).normalized;

            // Движение на слима към играча
            rb.velocity = moveDirection * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero; // Слимът спира да се движи, ако играчът не е намерен
        }
    }
}
