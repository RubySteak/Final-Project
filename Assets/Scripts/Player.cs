using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    public UnityEvent OnCrouchHitbox = new UnityEvent();
    public UnityEvent OffCrouchHitbox = new UnityEvent();
    
    Rigidbody2D rigidbody2D;
    public float jumpForce = 10.0f;
    public float jumpForceWall = 10.0f;
    public float jumpForceCrouch = 15.0f;
    public float moveSpeed = 5.0f;

    public bool isFalling = true;
    public bool isMoving = false;
    public bool isTouchingWall = false;
    public bool isCrouching = false;

    int midairJump = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        OnCrouchHitbox.AddListener(Listener);
        OffCrouchHitbox.AddListener(Listener);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        
        if(!isCrouching)
        {
            rigidbody2D.velocity = new Vector2(horizontalInput * moveSpeed, rigidbody2D.velocity.y);
        }
        

        isMoving = Mathf.Abs(horizontalInput) > 0.1f;

        if (!isFalling)
        {
            midairJump = 1;
        }

        if (isTouchingWall)
        {
            midairJump = 0;
        }

        if (!isFalling)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                isCrouching = true;
                rigidbody2D.velocity = new Vector2(0, 0);
                OnCrouchHitbox.Invoke();
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                isCrouching = false;
                OffCrouchHitbox.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isCrouching)
            {
                rigidbody2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                OffCrouchHitbox.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && isCrouching)
            {
                rigidbody2D.AddForce(Vector3.up * jumpForceCrouch, ForceMode2D.Impulse);
                isCrouching = false;
                OffCrouchHitbox.Invoke();
            }
        }
        else if (!isFalling || midairJump == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody2D.velocity = Vector2.zero;
                rigidbody2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                midairJump -= 1;
            }
        }

        if (isFalling && isTouchingWall)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody2D.AddForce(Vector3.up * jumpForceWall, ForceMode2D.Impulse);
                midairJump -= 1;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Ground"))
        {
            isFalling = false;
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
            isFalling = false;
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isFalling = true;
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
            isFalling = true;
        }
    }

    void Listener()
    {
        
    }
}
