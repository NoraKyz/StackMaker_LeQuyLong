using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region Singleton

    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataManager>();
            }

            return instance;
        }
    }

    #endregion
    
    public event Action<DataType, int> OnDataChanged;

    public int coins { get; private set; }
    public int level { get; private set; }
    
    #region Unity Function
    
    private void Awake()
    {
        instance = this;
        
        coins = PlayerPrefs.GetInt("Coins", 0);
        level = PlayerPrefs.GetInt("Level", 1);
    }
    
    #endregion
    
    public void SetData(DataType dataType, int value)
    {
        switch (dataType)
        {
            case DataType.Coin:
                SetCoin(value);
                break;
            case DataType.Level:
                SetLevel(value);
                break;
        }

        OnDataChanged?.Invoke(dataType, value);
    }
    
    private void SetCoin (int value)
    {
        coins = value;
        
        PlayerPrefs.SetInt("Coins", coins);
    }
    
    private void SetLevel (int value)
    {
        level = value;

        if (level > Constants.MAX_LEVEL)
        {
            level = 1;
        }
        
        PlayerPrefs.SetInt("Level", level);
    }
}
