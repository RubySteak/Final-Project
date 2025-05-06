using UnityEngine;
using UnityEngine.SceneManagement;

public class Deathtutorial : MonoBehaviour
{
    public static Death instance;

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DeathSceneTutorial()
    {
        Debug.Log("DeathSceneTutorial called");
        SceneManager.LoadScene(9);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollisionEnter2D called");
        if (other.gameObject.CompareTag("Player"))
        {
            DeathSceneTutorial();
        }
    }
}
