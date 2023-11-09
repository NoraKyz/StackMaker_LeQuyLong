using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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
    
    [SerializeField] Text coinText;
    [SerializeField] private List<GameObject> panels;

    public void SetCoinInfor(int coin)
    {
        coinText.text = coin.ToString();
    }

    private void OpenPanel(int index)
    {
        panels[index].SetActive(true);
    }

    private void ClosePanel(int index)
    { 
        panels[index].SetActive(false);
    }

    public void OpenWinPopup()
    {
        OpenPanel(1);
    }
    
    public void CloseWinPopup()
    {
        ClosePanel(1);
    }
}
