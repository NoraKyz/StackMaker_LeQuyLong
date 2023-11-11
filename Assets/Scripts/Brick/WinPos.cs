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

            StartCoroutine(ShowWinPopupCoroutine());
        }
    }
    
    private IEnumerator ShowWinPopupCoroutine()
    {
        yield return new WaitForSeconds(5f);
        UIManager.Instance.ShowWinPopup();
    }
}
