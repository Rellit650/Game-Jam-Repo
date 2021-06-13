using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitPickUp : MonoBehaviour
{
    PlayerSplitController splitRef;
    public int UISlotID;
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().SetPickUp(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().SetPickUp(null);
        }
    }
    public void PickUp() 
    {
        splitRef.splitsLeft++;
        splitRef.PickUpSplit(gameObject.transform.parent.parent.gameObject);
        Destroy(gameObject.transform.parent.parent.gameObject);
    }
}
