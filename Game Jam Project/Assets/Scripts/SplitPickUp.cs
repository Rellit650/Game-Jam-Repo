using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitPickUp : MonoBehaviour
{
    PlayerSplitController splitRef;
    private void Start()
    {
        splitRef = FindObjectOfType<PlayerSplitController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            other.GetComponent<PlayerMovement>().SetPickUp(this);
        }
    }

    public void PickUp() 
    {
        splitRef.splitsLeft++;
        Destroy(gameObject);
    }
}
