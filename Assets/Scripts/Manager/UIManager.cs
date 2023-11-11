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

    private void Awake()
    {
        instance = this;
    }

    #endregion
    
    [SerializeField] Text coinText;
    [SerializeField] private List<GameObject> panels;

    public void SetCoinInfor(int coin)
    {
        coinText.text = coin.ToString();
    }

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
}
