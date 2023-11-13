using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Singleton

    private static LevelManager instance;

    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();
            }

            return instance;
        }
    }

    #endregion
    

    [SerializeField] private List<GameObject> levelPrefabs = new List<GameObject>();
    
    private int currLevelId;
    private GameObject currentLevel;
    
    #region Unity Function

    private void Awake()
    {
        instance = this;
        currLevelId = PlayerPrefs.GetInt("Level", 1);
    }

    private void Start()
    {
        LoadLevel(currLevelId);
        
        GameManager.Instance.OnEventEmitted += OnEventEmitted;
    }

    #endregion
    
    private void OnEventEmitted(EventID eventID)
    {
        switch (eventID)
        {
            case EventID.OnNextLevel:
                LoadNextLevel();
                break;
        }
    }

    #region Other Functions

    private void LoadNextLevel()
    {
        if(currLevelId < Constants.MAX_LEVEL)
        {
            LoadLevel(currLevelId + 1);
        }
        else
        {
            LoadLevel(1);
        }
    }
    
    private void LoadLevel(int id)
    {
        currLevelId = id;
        UIManager.Instance.SetLevelInfor(id);
        PlayerPrefs.SetInt("Level", currLevelId);

        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        
        currentLevel = Instantiate(levelPrefabs[id - 1], transform);
    }

    #endregion
    
    
}
