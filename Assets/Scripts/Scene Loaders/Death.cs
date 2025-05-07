using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public static Death instance;

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DeathScene()
    {
        SceneManager.LoadScene("Death");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DeathScene();
        }
    }
}
