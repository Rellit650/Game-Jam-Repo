using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerStateController : MonoBehaviour
{
    public int state = 0;
    public GameObject[] stateGameObjectArray;
    CinemachineFreeLook cmCamera;

    private void Start()
    {
        cmCamera = FindObjectOfType<CinemachineFreeLook>();
    }
    public void ChangeToNextState() 
    {
        if (state == 0)
        {
            stateGameObjectArray[1].transform.position = stateGameObjectArray[0].transform.position;
            stateGameObjectArray[0].SetActive(false);
            stateGameObjectArray[1].SetActive(true);
            
            cmCamera.Follow = stateGameObjectArray[1].transform;
            cmCamera.LookAt = stateGameObjectArray[1].transform;
        }
        else if (state == 1) 
        {
            stateGameObjectArray[2].transform.position = stateGameObjectArray[1].transform.position;
            stateGameObjectArray[1].SetActive(false);
            stateGameObjectArray[2].SetActive(true);
            
            cmCamera.Follow = stateGameObjectArray[2].transform;
            cmCamera.LookAt = stateGameObjectArray[2].transform;
        }
        else if (state == 2)
        {
            stateGameObjectArray[0].transform.position = stateGameObjectArray[2].transform.position;
            stateGameObjectArray[2].SetActive(false);
            stateGameObjectArray[0].SetActive(true);
            
            cmCamera.Follow = stateGameObjectArray[0].transform;
            cmCamera.LookAt = stateGameObjectArray[0].transform;
        }
        state++;
        if (state >= 3) 
        {
            state = 0;
        }
    }

    public void SetState(int newState) 
    {
        stateGameObjectArray[state].SetActive(false);
        state = newState;
        stateGameObjectArray[state].SetActive(true);
    }
}
