using UnityEngine;

public class NextLevelUnlock : MonoBehaviour
{

    [Header("Level Map's Components")]
    [Space(10)]
    [Tooltip("Components to display locked state")]
    public GameObject[] levelIsLocked;
    [Tooltip("Components to display unlocked state")]
    public GameObject[] levelUnlocked;

    private int completedLevelIndex;


    private void Start()
    {
         // Get index of complete level from PlayerPrefs.
         completedLevelIndex = PlayerPrefs.GetInt("Level");

         Debug.Log("Completed level = " + completedLevelIndex);

         UnlockedNextLevel();
        
    }


    private void UnlockedNextLevel()
    {
        // When finished level completedRaces value changed and set that value to completedLevelIndex if it lower.
        if (completedLevelIndex < GameData.Instance.completedRaces)
            completedLevelIndex = GameData.Instance.completedRaces;

        // Looping levels opening in LevelChooseMenu if completed some level.
        for (int i = 0; i < completedLevelIndex; i++)
        {
            if (PlayerPrefs.HasKey("Level") && (completedLevelIndex >= GameData.Instance.completedRaces)
                && (i < levelIsLocked.Length) || ( i < levelUnlocked.Length))
            {
                if (levelIsLocked[i] != null && levelUnlocked[i] != null)
                {
                    Debug.Log("UnlockedLevel: " + (i + 2));
                    levelIsLocked[i].SetActive(false);
                    levelUnlocked[i].SetActive(true);
                } else
                {
                    continue;
                }
            }
        }
    }
}
