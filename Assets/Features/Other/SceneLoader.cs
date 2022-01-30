using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        LoadNext();
    }

    public void LoadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadFirst()
    {
        SceneManager.LoadScene(0);
    }
    
}
