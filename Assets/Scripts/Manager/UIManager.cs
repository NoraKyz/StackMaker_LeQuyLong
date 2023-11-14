using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }
    
    #endregion
    
    [SerializeField] private Text coinText;
    [SerializeField] private Text levelText;
    [SerializeField] private List<GameObject> panels;

    #region Unity Function

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        HidePanel(1);
        
        GameManager.Instance.OnEventEmitted += OnEventEmitted;
        DataManager.Instance.OnDataChanged += OnDataChanged;
    }

    #endregion
    
    private void OnEventEmitted(EventID eventId)
    {
        switch (eventId)
        {
            case EventID.OnCompleteLevel:
                ShowWinPopup();
                break;
            case EventID.OnNextLevel:
                HideWinPopup();
                break;
        }
    }
    
    private void OnDataChanged(DataType dataType, int value)
    {
        switch (dataType)
        {
            case DataType.Coin:
                SetCoinInfor(value);
                break;
            case DataType.Level:
                SetLevelInfor(value);
                break;            
        }
    }

    #region Other Functions

    private void ShowPanel(int index)
    {
        panels[index].SetActive(true);
    }

    private void HidePanel(int index)
    { 
        panels[index].SetActive(false);
    }

    private void ShowWinPopup()
    {
        ShowPanel(1);
    }
    
    private void HideWinPopup()
    {
        HidePanel(1);
    }
    
    public void SetCoinInfor(int coin)
    {
        coinText.text = coin.ToString();
    }
    
    public void SetLevelInfor(int level)
    {
        levelText.text = "Level " + level;
    }

    #endregion
}
