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

            StartCoroutine(OnCompleteLevel());
        }
    }
    
    private IEnumerator OnCompleteLevel()
    {
        yield return new WaitForSeconds(5f);
        GameManager.Instance.EmitOnCompleteLevelEvent();
    }
}
