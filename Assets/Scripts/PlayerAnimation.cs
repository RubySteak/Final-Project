using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    Player player;
    Rigidbody2D rigidbody2D;

    bool isTouchingWall;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Falling", player.isFalling);
        animator.SetBool("Running", player.isMoving);
        animator.SetBool("TouchingWall", isTouchingWall);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
        }
    }
}
