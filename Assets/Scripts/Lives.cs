using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lives : MonoBehaviour
{
    [SerializeField] int lives;

    public UnityEvent LivesCheck = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        LivesCheck.AddListener(listener);
    }



    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
        {
            LivesCheck.Invoke();
        }
        else
        {
            lives--;
        }
    }

    void listener()
    {
        Debug.Log("Game Over");
    }
}