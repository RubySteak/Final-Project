using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool isPause;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PauseScreen();
    }

    void PauseScreen()
    {
        if (Input.GetKeyDown(KeyCode.P) && isPause == false)
        {
            Time.timeScale = 0;
            isPause = true;
        }

        if (Input.GetKeyDown(KeyCode.P) && isPause == true)
        {
            Time.timeScale = 1;
            isPause = false;
        }
    }
}
