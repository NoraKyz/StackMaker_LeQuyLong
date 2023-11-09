using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBrick : MonoBehaviour
{
    [SerializeField] private Material lastBrickMaterial;
    private bool isTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger)
        {
            return;
        }
        
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().RemoveBrick();
            GetComponent<MeshRenderer>().material = lastBrickMaterial;
            isTrigger = true;
        }
    }
}
