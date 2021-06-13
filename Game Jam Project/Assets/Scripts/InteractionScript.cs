using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    public enum State 
    {
        Solid,
        Liquid,
        Gas
    }
    [Tooltip("What State is required to active the GameObject")]
    public State interactionState;
    public GameObject activateThis;

    PlayerSplitController splitRef;
    CameraTransitionScript CTS;

    private void Start()
    {
        splitRef = FindObjectOfType<PlayerSplitController>();
        CTS = FindObjectOfType<CameraTransitionScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Split")) 
        {
            if (other.transform.parent.GetComponent<PlayerStateController>().state == (int)interactionState) 
            {
                //Activate the object linked to this interaction Spot
                activateThis.SetActive(true);

                //Handle split removal from list 
                splitRef.PickUpSplit(other.transform.parent.gameObject);

                //Cycle off of this split to next avaible one
                other.GetComponent<PlayerMovement>().SwitchPlayerControl();

                //Destroy split used for interaction
                //splitRef.splitsLeft++;
                Destroy(other.transform.parent.gameObject);

                //Turn off this object once we have interacted with it
                gameObject.SetActive(false);
                //Turn off the parent object once we have interacted with it
                gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Split"))
        {
            if (other.transform.parent.GetComponent<PlayerStateController>().state == (int)interactionState)
            {
                //Activate the object linked to this interaction Spot
                activateThis.SetActive(true);
                //Handle split removal from list 
                splitRef.PickUpSplit(other.transform.parent.gameObject);
                //Cycle off of this split to next avaible one
                other.GetComponent<PlayerMovement>().SwitchPlayerControl();      

                //Destroy split used for interaction
                //splitRef.splitsLeft++;
                Destroy(other.transform.parent.gameObject);

                //Turn off this object once we have interacted with it
                gameObject.SetActive(false);
                //Turn off the parent object once we have interacted with it
                gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
