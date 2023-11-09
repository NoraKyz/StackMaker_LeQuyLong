using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private Transform brickOnTop;
    private bool isTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger)
        {
            return;
        }
        
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().AddBrick();
            brickOnTop.gameObject.SetActive(false);
            isTrigger = true;
        }
    }
}
