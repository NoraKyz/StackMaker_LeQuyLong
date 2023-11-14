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

    private int currentLevelID;
    private GameObject currentLevel;
    
    #region Unity Function

    private void Awake()
    {
        instance = this;

        currentLevelID = DataManager.Instance.level;
    }

    private void Start()
    {
        LoadLevel(currentLevelID);

        DataManager.Instance.OnDataChanged += OnDataChanged;
    }

    #endregion

    #region Other Functions
    
    private void OnDataChanged(DataType dataType, int value)
    {
        if(dataType == DataType.Level)
        {
            LoadLevel(value);
        }
    }
    
    private void LoadLevel(int id)
    {
        currentLevelID = id;

        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        
        currentLevel = Instantiate(levelPrefabs[id - 1], transform);
    }

    #endregion
    
    
}
