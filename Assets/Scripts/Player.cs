using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float jumpForce = 10.0f;
    public float moveSpeed = 5.0f;

    public bool isFalling = true;
    public bool isMoving = false; // New bool for movement

    int midairJump = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(horizontalInput * moveSpeed, rigidbody2D.velocity.y);

        // Update isMoving based on horizontal input
        isMoving = Mathf.Abs(horizontalInput) > 0.1f;

        // Reset midair jump when not falling
        if (!isFalling)
        {
            midairJump = 1;
        }

        // Jump logic
        if (!isFalling)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        else if (!isFalling || midairJump == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                midairJump -= 1;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        print("Touching ground");
        if (other.gameObject.CompareTag("Ground"))
        {
            isFalling = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isFalling = true;
        }
    }
}
