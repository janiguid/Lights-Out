using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Level[] AvailableLevels;

    public int CurrentLevel;
    public int PreviousLevel;

    public int PreviousStartingPoint;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public int GetSceneNumber()
    {
        for(int i = 0; i < AvailableLevels.Length; ++i)
        {
            if(AvailableLevels[i].LevelNumber == CurrentLevel)
            {
                return AvailableLevels[i].SceneNumber;
            }
        }

        print("Failed to find scene");
        return 0;
    }
}

[Serializable]
public class Level
{
    public int SceneNumber;
    public int LevelNumber;
}