using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Collider2D platformCollider;

    [SerializeField]
    private float disableDuration = 0.5f; // Duration for which the collider is disabled

    private bool playerOnPlatform = false; // Tracks if the player is on the platform

    // Start is called before the first frame update
    void Start()
    {
        platformCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnPlatform && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(DisableCollider());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }

    private IEnumerator DisableCollider()
    {
        platformCollider.enabled = false;
        yield return new WaitForSeconds(disableDuration);
        platformCollider.enabled = true;
    }
}
