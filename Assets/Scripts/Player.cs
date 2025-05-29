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
    public float jumpForceSpacePlatform = 2.5f;
    public float jumpForceCrouch = 15.0f;
    public float jumpForceCrouchSpace = 5.0f;
    public float springForceSpace = 30.0f;
    public float springForce = 20.0f;
    public float moveSpeed = 5.0f;
    public float moveSpeedSpace = 2.0f;

    public bool isFalling = true;
    public bool isMoving = false;
    public bool isTouchingWall = false;
    public bool isCrouching = false;
    public bool isSpringSpace = false;
    public bool isSpring = false;
    public bool isOnSpacePlatform = false;
    public bool isInSpace = false;

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

        if (!isCrouching && !isInSpace)
        {
            rigidbody2D.velocity = new Vector2(horizontalInput * moveSpeed, rigidbody2D.velocity.y);
        }
        else if (!isCrouching && isInSpace)
        {
            rigidbody2D.velocity = new Vector2(horizontalInput * moveSpeedSpace, rigidbody2D.velocity.y);
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

        if (!isFalling) // if not falling 
        {
            if (Input.GetKeyDown(KeyCode.S)) // if presses S key
            {
                isCrouching = true;
                rigidbody2D.velocity = new Vector2(0, 0);
                OnCrouchHitbox.Invoke();
            }
            else if (Input.GetKeyUp(KeyCode.S)) // if releases S key
            {
                isCrouching = false;
                OffCrouchHitbox.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isCrouching && !isInSpace) // if not crouching and presses space
            {
                rigidbody2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                OffCrouchHitbox.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && isInSpace && !isCrouching) // if in space, not crouching and presses space
            {
                rigidbody2D.AddForce(Vector3.up * jumpForceSpacePlatform, ForceMode2D.Impulse);
                OffCrouchHitbox.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Space) && isCrouching && !isInSpace) // if crouching and presses space
            {
                rigidbody2D.AddForce(Vector3.up * jumpForceCrouch, ForceMode2D.Impulse);
                isCrouching = false;
                OffCrouchHitbox.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && isCrouching && isInSpace) // if in space, crouching and presses space
            {
                rigidbody2D.AddForce(Vector3.up * jumpForceCrouchSpace, ForceMode2D.Impulse);
                isCrouching = false;
                OffCrouchHitbox.Invoke();
            }
        }
        else if (!isFalling || midairJump == 1) // if not falling or has midair jumps left
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isInSpace)
            {
                rigidbody2D.velocity = Vector2.zero;
                rigidbody2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                midairJump -= 1;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && isInSpace)
            {
                rigidbody2D.velocity = Vector2.zero;
                rigidbody2D.AddForce(Vector3.up * jumpForceSpacePlatform, ForceMode2D.Impulse);
                midairJump -= 1;
            }
        }

        if (isFalling && isTouchingWall) // scrapted wall jump
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody2D.AddForce(Vector3.up * jumpForceWall, ForceMode2D.Impulse);
                midairJump -= 1;
            }
        }

        if (isSpringSpace) // if touching spring
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.AddForce(Vector3.up * springForceSpace, ForceMode2D.Impulse);
        }

        if (isSpring)
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.AddForce(Vector3.up * springForceSpace, ForceMode2D.Impulse);
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

        if (other.gameObject.CompareTag("Spring"))
        {
            isSpringSpace = true;
            midairJump = 0;
        }

        if (other.gameObject.CompareTag("Spring normal"))
        {
            isSpring = true;
            midairJump = 0;
        }

        if (other.gameObject.CompareTag("Space Wall"))
        {
            isFalling = false;
            isOnSpacePlatform = true;
            midairJump = 1;
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

        if (other.gameObject.CompareTag("Spring"))
        {
            isSpringSpace = false;
            midairJump = 0;
        }

        if (other.gameObject.CompareTag("Spring normal"))
        {
            isSpring = false;
            midairJump = 0;
        }

        if (other.gameObject.CompareTag("Space Wall"))
        {
            isFalling = true;
            isOnSpacePlatform = false;
        }
    }

    void Listener()
    {

    }

    public void SpaceTrigger()
    {
        isInSpace = true;
    }
}
