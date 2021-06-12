using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSplitController : MonoBehaviour
{
    public int splitsLeft;
    [SerializeField]
    GameObject splitPrefab;

    List<GameObject> SplitHolder;

    PlayerStateController stateRef;
    private void Start()
    {
        SplitHolder = new List<GameObject>();
        stateRef = FindObjectOfType<PlayerStateController>();
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
}
