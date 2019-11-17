using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float acceleration;
    public float topSpeed;
    public float jumpForce;

    private Rigidbody2D characterRigidBody;

    private bool _isTouchingGround;
    // Start is called before the first frame update
    void Start()
    {
        characterRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Move();
    }

    private void Jump()
    {
        if (_isTouchingGround)
        {
            var jumpInput = Input.GetButtonDown("Jump");

            if (jumpInput)
            {
                characterRigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    private void Move()
    {
        if (_isTouchingGround)
        {
            var horizontalForce = Input.GetAxis("Horizontal");

            var velocity = new Vector2(horizontalForce * Time.deltaTime * acceleration, 0f);

            if ((Mathf.Abs(characterRigidBody.velocity.x) < topSpeed))
            {
                characterRigidBody.AddForce(velocity, ForceMode2D.Impulse);
            }
            else
            {
                characterRigidBody.velocity = new Vector2(topSpeed * Mathf.Sign(velocity.x), characterRigidBody.velocity.y);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            _isTouchingGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            _isTouchingGround = false;
        }
    }
}
