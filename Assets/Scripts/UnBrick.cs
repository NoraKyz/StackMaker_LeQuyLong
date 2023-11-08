using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBrick : MonoBehaviour
{
    [SerializeField] private GameObject lastBrickPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().RemoveBrick();
            Instantiate(lastBrickPrefab, transform.position, lastBrickPrefab.transform.rotation);
            Destroy(gameObject);
        }
    }
}
