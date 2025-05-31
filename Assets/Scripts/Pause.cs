using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Pause : MonoBehaviour
{
    bool isPause;

    public UnityEvent PauseEvent = new UnityEvent();
    public UnityEvent UnpauseEvent = new UnityEvent();
    
    // Start is called before the first frame update
    void Start()
    {
        PauseEvent.AddListener(Listener);
        UnpauseEvent.AddListener(Listener);
    }

    // Update is called once per frame
    void Update()
    {
       PauseCheck();
    }

    void PauseCheck()
    {
        
        if (Input.GetKeyDown(KeyCode.P) && isPause == false)
        {
            Time.timeScale = 0;
            isPause = true;
            Debug.Log("Game Paused");
            PauseEvent.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPause == true)
        {
            Time.timeScale = 1;
            isPause = false;
            Debug.Log("Game Unpaused");
            UnpauseEvent.Invoke();
        }
    }

    void Listener()
    {
        
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }
}
