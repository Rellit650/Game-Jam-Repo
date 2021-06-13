using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZoneScript : MonoBehaviour
{
    [SerializeField]
    private int splitsRequired;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (other.transform.parent.GetComponent<PlayerSplitController>().splitsLeft >= splitsRequired) 
            {
                Debug.Log("You did It! Wow!");
            }           
        }
    }
}
