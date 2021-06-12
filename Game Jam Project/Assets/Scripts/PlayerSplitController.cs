using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSplitController : MonoBehaviour
{
    public int splitsLeft;
    [SerializeField]
    GameObject splitPrefab;

    List<GameObject> SplitHolder;

    int currentSplitIndex = 0;

    PlayerStateController stateRef;
    private void Start()
    {
        SplitHolder = new List<GameObject>();
        stateRef = FindObjectOfType<PlayerStateController>();
        SplitHolder.Add(gameObject);
        //playerRef = FindObjectOfType<PlayerMovement>().gameObject;
    }
    public void SplitPlayer(GameObject player) 
    {
        if (splitsLeft > 0) 
        {
            Transform playerTransform = player.transform;
            if (stateRef.state == 0)
            {
                GameObject temp = Instantiate(splitPrefab, playerTransform.position + new Vector3(0f, 1f, 0f), playerTransform.rotation);
                temp.GetComponent<PlayerStateController>().SetState(0);
                SplitHolder.Add(temp);
            }
            if (stateRef.state == 1)
            {
                GameObject temp = Instantiate(splitPrefab, playerTransform.position + new Vector3(0f, 1f, 0f), playerTransform.rotation);
                temp.GetComponent<PlayerStateController>().SetState(1);
                SplitHolder.Add(temp);
            }
            if (stateRef.state == 2)
            {
                GameObject temp = Instantiate(splitPrefab, playerTransform.position + new Vector3(0f, 1f, 0f), playerTransform.rotation);
                temp.GetComponent<PlayerStateController>().SetState(2);
                SplitHolder.Add(temp);
            }
            --splitsLeft;
        }        
    }

    public void CycleControl() 
    {
        for (int i = 0; i < SplitHolder[currentSplitIndex].transform.childCount; i++) 
        {
            Transform child = SplitHolder[currentSplitIndex].transform.GetChild(i);
            bool curActive = child.gameObject.activeSelf;
            child.gameObject.SetActive(true);
            child.gameObject.GetComponent<PlayerMovement>().enabled = false;
            child.gameObject.SetActive(curActive);
        }
        if (currentSplitIndex >= SplitHolder.Count-1)
        {
            currentSplitIndex = 0;
        }
        else 
        {
            currentSplitIndex++;
        }
        Transform objectToView = gameObject.transform;
        for (int i = 0; i < SplitHolder[currentSplitIndex].transform.childCount; i++) 
        {
            Transform child = SplitHolder[currentSplitIndex].transform.GetChild(i);
            bool curActive = child.gameObject.activeSelf;
            child.gameObject.SetActive(true);
            child.gameObject.GetComponent<PlayerMovement>().enabled = true;
            child.gameObject.SetActive(curActive);
            if (curActive) 
            {
                objectToView = child;
            }
        }
        CinemachineFreeLook cmCamera = FindObjectOfType<CinemachineFreeLook>();
        cmCamera.Follow = objectToView;
        cmCamera.LookAt = objectToView;
    }

    public void PickUpSplit(GameObject split) 
    {
        SplitHolder.Remove(split);
        if (currentSplitIndex >= SplitHolder.Count - 1) 
        {
            currentSplitIndex = 0;
        }
    }
}
