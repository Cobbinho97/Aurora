using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 10f;

    public Rigidbody2D rb;

    public Transform feet;

    public LayerMask groundLayers;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        var movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * movementSpeed;

        if(Input.GetKeyDown(KeyCode.UpArrow) && isGrounded())
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, 20f), ForceMode2D.Impulse);
    }

    public bool isGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);

        if(groundCheck != null)
        {
            return true;
        }
        return false;
    }
}
