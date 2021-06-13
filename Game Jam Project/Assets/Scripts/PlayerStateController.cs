using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerStateController : MonoBehaviour
{
    public int state = 0;
    public GameObject[] stateGameObjectArray;
    CinemachineFreeLook cmCamera;
    public int UISlotID;
    UIScript UIRef;
    CameraTransitionScript CTS;

    private void Start()
    {
        cmCamera = FindObjectOfType<CinemachineFreeLook>();
        UIRef = FindObjectOfType<UIScript>();
        CTS = FindObjectOfType<CameraTransitionScript>();
    }
    public void ChangeToNextState() 
    {
        CTS.enabled = true;
        
        if (state == 0)
        {
            stateGameObjectArray[1].transform.position = stateGameObjectArray[0].transform.position;
            stateGameObjectArray[0].SetActive(false);
            stateGameObjectArray[1].SetActive(true);

            CTS.SetTarget(stateGameObjectArray[1].transform);
            cmCamera.Follow = CTS.transform;
            cmCamera.LookAt = CTS.transform;
            cmCamera.GetRig(0).LookAt = stateGameObjectArray[1].transform.Find("TopRigOverrideTarget");
            cmCamera.GetRig(1).LookAt = stateGameObjectArray[1].transform.Find("MiddleRigOverrideTarget");
            cmCamera.GetRig(2).LookAt = stateGameObjectArray[1].transform.Find("BottomRigOverrideTarget");
            
        }
        else if (state == 1) 
        {
            stateGameObjectArray[2].transform.position = stateGameObjectArray[1].transform.position;
            stateGameObjectArray[1].SetActive(false);
            stateGameObjectArray[2].SetActive(true);

            CTS.SetTarget(stateGameObjectArray[2].transform);
            cmCamera.Follow = CTS.transform;
            cmCamera.LookAt = CTS.transform;
            cmCamera.GetRig(0).LookAt = stateGameObjectArray[2].transform.Find("TopRigOverrideTarget");
            cmCamera.GetRig(1).LookAt = stateGameObjectArray[2].transform.Find("MiddleRigOverrideTarget");
            cmCamera.GetRig(2).LookAt = stateGameObjectArray[2].transform.Find("BottomRigOverrideTarget");
        }
        else if (state == 2)
        {
            stateGameObjectArray[0].transform.position = stateGameObjectArray[2].transform.position;
            stateGameObjectArray[2].SetActive(false);
            stateGameObjectArray[0].SetActive(true);

            CTS.SetTarget(stateGameObjectArray[0].transform);
            cmCamera.Follow = CTS.transform;
            cmCamera.LookAt = CTS.transform;
            cmCamera.GetRig(0).LookAt = stateGameObjectArray[0].transform.Find("TopRigOverrideTarget");
            cmCamera.GetRig(1).LookAt = stateGameObjectArray[0].transform.Find("MiddleRigOverrideTarget");
            cmCamera.GetRig(2).LookAt = stateGameObjectArray[0].transform.Find("BottomRigOverrideTarget");
        }
        ++state;
        if (state >= 3) 
        {
            state = 0;
        }
        if (gameObject.CompareTag("Split"))
        {
            UIRef.UpdateSplitIcon(UISlotID, state);
        }
        if (gameObject.CompareTag("Player")) 
        {
            UIRef.SetPlayerImage(state);
        }
    }

    public void SetState(int newState) 
    {
        stateGameObjectArray[state].SetActive(false);
        state = newState;
        stateGameObjectArray[state].SetActive(true);
    }
}
