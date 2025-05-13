using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DeathScreenLoad : MonoBehaviour
{
    public UnityEvent DeathScreenLoader = new UnityEvent();

    private Collider2D DeathScreenTrigger;

    private bool isPlayerOnTrigger = false;
    
    // Start is called before the first frame update
    void Start()
    {
        DeathScreenTrigger = GetComponent<Collider2D>();

        DeathScreenLoader.AddListener(Listener);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isPlayerOnTrigger) 
        {
            DeathScreenLoader.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("square trigger"))
        {
            isPlayerOnTrigger = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("square trigger"))
        {
            isPlayerOnTrigger = false;
        }
    }

    void Listener()
    {
        Debug.Log("Player has died");
    }
}
