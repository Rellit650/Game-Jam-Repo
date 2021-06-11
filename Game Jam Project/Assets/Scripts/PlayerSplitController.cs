using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSplitController : MonoBehaviour
{
    public int splitsLeft;
    [SerializeField]
    GameObject splitSolidPrefab;
    [SerializeField]
    GameObject splitLiquidPrefab;
    [SerializeField]
    GameObject splitGasPrefab;

    PlayerStateController stateRef;
    private void Start()
    {
        stateRef = FindObjectOfType<PlayerStateController>();
    }
    public void SplitPlayer() 
    {
        if (splitsLeft > 0) 
        {
            if (stateRef.state == 0)
            {
                Instantiate(splitSolidPrefab);
            }
            if (stateRef.state == 1)
            {
                Instantiate(splitLiquidPrefab);
            }
            if (stateRef.state == 2)
            {
                Instantiate(splitGasPrefab);
            }
        }        
    }
}
