using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLevel : MonoBehaviour
{
    [SerializeField] private int levelIndex;

    public int lastLevelIndexInFirstMap;
    public int lastLevelIndexInSecondMap;

    public int LevelIndex { get { return levelIndex; } }

    private void OnEnable()
    {
        Events.OnStopRace += LevelComplete;
    }

    private void OnDisable()
    {
        Events.OnStopRace -= LevelComplete;
    }

    private void LevelComplete()
    {
        if (levelIndex > PlayerPrefs.GetInt("Level", levelIndex))
        {
            PlayerPrefs.SetInt("Level", levelIndex);
            GameData.Instance.completedRaces = levelIndex; 
        }

        Debug.Log("Level " + GameData.Instance.completedRaces + " is complete");
    }
}
