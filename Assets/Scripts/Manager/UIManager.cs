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
    }

    #endregion
    
    private void ShowPanel(int index)
    {
        panels[index].SetActive(true);
    }

    private void HidePanel(int index)
    { 
        panels[index].SetActive(false);
    }

    public void ShowWinPopup()
    {
        ShowPanel(1);
    }
    
    public void HideWinPopup()
    {
        HidePanel(1);
    }
    
    public void SetCoinInfor(int coin)
    {
        coinText.text = coin.ToString();
    }
    
    public void SetLevelInfor(int level)
    {
        levelText.text = level.ToString();
    }
}
