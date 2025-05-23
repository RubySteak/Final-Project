using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DeathScreen : MonoBehaviour
{
    public UnityEvent OnDeathEvent = new UnityEvent();

    private Collider2D DeathScreenTrigger;

    private bool isPlayerOnTrigger = false;
    
    // Start is called before the first frame update
    void Start()
    {
        DeathScreenTrigger = GetComponent<Collider2D>();

        OnDeathEvent.AddListener(Listener);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isPlayerOnTrigger) 
        {
            OnDeathEvent.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerOnTrigger = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerOnTrigger = false;
        }
    }

    void Listener()
    {
        
    }
}
