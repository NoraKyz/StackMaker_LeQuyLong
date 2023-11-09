using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().ClearBrick();
            
            Invoke(nameof(ShowWinPopup), 5f);
        }
    }
    
    private void ShowWinPopup()
    {
        UIManager.Instance.OpenWinPopup();
    }
}
