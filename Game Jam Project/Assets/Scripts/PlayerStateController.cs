using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public int state = 0;
    public GameObject[] stateGameObjectArray;

    public void ChangeToNextState() 
    {
        if (state == 0)
        {
            stateGameObjectArray[1].transform.position = stateGameObjectArray[0].transform.position;
            stateGameObjectArray[0].SetActive(false);
            stateGameObjectArray[1].SetActive(true);
        }
        else if (state == 1) 
        {
            stateGameObjectArray[2].transform.position = stateGameObjectArray[1].transform.position;
            stateGameObjectArray[1].SetActive(false);
            stateGameObjectArray[2].SetActive(true);
        }
        else if (state == 2)
        {
            stateGameObjectArray[0].transform.position = stateGameObjectArray[2].transform.position;
            stateGameObjectArray[2].SetActive(false);
            stateGameObjectArray[0].SetActive(true);
        }
        state++;
        if (state >= 3) 
        {
            state = 0;
        }
    }
}
