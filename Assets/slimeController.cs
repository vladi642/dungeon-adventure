using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeController : MonoBehaviour
{
    public Animator animator;

    public Transform player; // ���������� �� ������

    public float moveSpeed = 5f; // ������� �� �������� �� �����

    private Rigidbody2D rb; // Rigidbody ��������� �� �����

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // ���������� �� Rigidbody ���������
    }

    private void Update()
    {
        if (player != null) // ��� ������� ����������
        {
            // ����������� �� �������� ��� ������
            Vector2 moveDirection = (player.position - transform.position).normalized;

            // �������� �� ����� ��� ������
            rb.velocity = moveDirection * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero; // ������ ����� �� �� �����, ��� ������� �� � �������
        }
    }
}
