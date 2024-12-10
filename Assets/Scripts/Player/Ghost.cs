using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GhostState State;
    public float moveSpeed = 5f; // Speed at which the object moves
    public float rotationSpeed = 5f; // Speed at which the object rotates towards the player
    private Transform player; // Reference to the "Player" object
    private Rigidbody2D rb; // Rigidbody2D for movement
    private Vector2 moveDirection; // Direction of movement

    public enum GhostState
    {
        Chasing,
        Frozen
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Find the player by tag
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        if (State == GhostState.Chasing && player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveDirection = direction;
        }
    }

    void FixedUpdate()
    {
        // Move the ghost only when in the Chasing state
        rb.velocity = moveDirection * (State == GhostState.Chasing ? moveSpeed : 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flashlight"))
        {
            SetState(GhostState.Frozen);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Flashlight"))
        {
            SetState(GhostState.Chasing);
        }
    }

    void Frozen()
    {
        moveDirection = Vector2.zero; // Stop movement
        rb.velocity = Vector2.zero; // Ensure immediate stop
        Debug.Log("Ghost is frozen!");
    }

    void SetState(GhostState state)
    {
        if (state == State)
        {
            return; // No need to change if the state is already the same
        }

        if (state == GhostState.Chasing && player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveDirection = direction;
        }
        else if (state == GhostState.Frozen)
        {
            Frozen();
        }

        State = state; // Update the state
    }
}