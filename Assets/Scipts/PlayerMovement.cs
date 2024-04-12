using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    public Animator animator;

    private float buttonHoldTime = 0f;
    private Vector2 previousMovement;

    private void Start()
    {
        
    }

    void Update()
    {
        // Calculate movement input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Set animator parameters based on movement
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Speed", Mathf.Sqrt(horizontal * horizontal + vertical * vertical));
        // rb.velosity.sqrmagnitude

        // Check if the player is holding a button
        if (horizontal != 0f || vertical != 0f)
        {
            // Increment the button hold time
            buttonHoldTime += Time.deltaTime;
        }
        else
        {
            // Log the button hold time and reset when the player releases the button
            if (buttonHoldTime > 0f)
            {
                Debug.Log("Player held the button for " + buttonHoldTime + " seconds. Direction: " + previousMovement);
                buttonHoldTime = 0f;
            }
        }

        // Update the previous movement
        previousMovement = new Vector2(horizontal, vertical);
    }

    void FixedUpdate()
    {
        // Move the player based on the current movement input
        rb.MovePosition(rb.position + previousMovement * moveSpeed * Time.fixedDeltaTime);
        
        // Use raycast to find the position of the mouse in world coordinates
        ///////////Ray ray = cam.ScreenPointToRay(Input.mousePosition);
      ///////////  RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

       // if (hit.collider != null)
        {
            // Process information about the hit object if needed
          //////////  Debug.Log("Hit object: " + hit.collider.name);

            // Example: Rotate the player towards the mouse position
           // ///////////Vector2 lookDir = hit.point - rb.position;
           // ////////////float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
           // ///////////rb.rotation = angle;
        }
    }
}