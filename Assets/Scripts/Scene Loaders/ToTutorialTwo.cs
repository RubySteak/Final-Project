using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTutorialTwo : MonoBehaviour
{
    public static ToTutorialTwo instance;

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ToTutorial()
    {
        Debug.Log("DeathSceneTutorial called");
        SceneManager.LoadScene("Tutorial2");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollisionEnter2D called");
        if (other.gameObject.CompareTag("square trigger"))
        {
            ToTutorial();
        }
    }
}
