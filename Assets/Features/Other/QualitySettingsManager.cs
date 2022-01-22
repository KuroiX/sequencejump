using UnityEngine;
using UnityEngine.UI;

public class QualitySettingsManager : MonoBehaviour
{
    [SerializeField] private Text qualityLevel;

    private void Awake()
    {
        string[] names = QualitySettings.names;
        int currentLevel = QualitySettings.GetQualityLevel();

        qualityLevel.text = names[currentLevel];
    }

    public void XcrementQualitySettings(bool increment)
    {
        string[] names = QualitySettings.names;
        int currentLevel = QualitySettings.GetQualityLevel();

        int targetLevel = currentLevel + (increment ? 1 : -1);

        targetLevel = targetLevel < 0 ? 0 : (targetLevel >= names.Length ? names.Length-1 : targetLevel);

        QualitySettings.SetQualityLevel(targetLevel, true);
        
        qualityLevel.text = names[targetLevel];
    }
}
