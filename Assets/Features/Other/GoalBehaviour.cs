using Features.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalBehaviour : MonoBehaviour
{
    [SerializeField] private LevelContainer level;

    private void OnTriggerEnter2D(Collider2D col)
    {
        level.isCleared = true;
        
        if (!ReferenceEquals(level.nextLevel, null))
        {
            level.nextLevel.isUnlocked = true;
        }

        SceneManager.LoadScene(0);
    }
}
