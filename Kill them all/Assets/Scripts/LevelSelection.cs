using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    public GameObject[] levels;

    private int currentLevel;

    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("LevelsCompleted", 1);

        for (int i = 0; i < currentLevel; i++)
        {
            levels[i].SetActive(true);
        }
    }

}
