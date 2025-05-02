using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float jumpForce = 10.0f;
    public float jumpForceWall = 10.0f;
    public float moveSpeed = 5.0f;

    public bool isFalling = true;
    public bool isMoving = false;
    public bool isTouchingWall = false;

    int midairJump = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(horizontalInput * moveSpeed, rigidbody2D.velocity.y);

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

        if (isFalling && isTouchingWall)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody2D.AddForce(Vector3.up * jumpForceWall, ForceMode2D.Impulse);
                midairJump -= 1;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Ground"))
        {
            print("Touching ground");
            isFalling = false;
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            print("Touching wall");
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
}
