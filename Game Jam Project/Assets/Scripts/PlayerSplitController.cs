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
    CameraTransitionScript CTS;
    CinemachineFreeLook cmCamera;

    private void Start()
    {
        SplitHolder = new List<GameObject>();
        stateRef = FindObjectOfType<PlayerStateController>();
        CTS = FindObjectOfType<CameraTransitionScript>();
        SplitHolder.Add(gameObject);
        cmCamera = FindObjectOfType<CinemachineFreeLook>();
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

    public void CycleControl(AudioSource old, AudioScript handler, float newVolume) 
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
            if (child.gameObject.CompareTag("Split"))
            {
                child.gameObject.GetComponent<PlayerMovement>().SetSplitBool(true);
            }
            child.gameObject.SetActive(curActive);
            if (curActive) 
            {
                objectToView = child;
            }
        }
        CTS.enabled = true;
        CTS.SetTarget(objectToView);
        cmCamera.Follow = CTS.transform;
        cmCamera.LookAt = CTS.transform;
        cmCamera.GetRig(0).LookAt = objectToView.Find("TopRigOverrideTarget");
        cmCamera.GetRig(1).LookAt = objectToView.Find("MiddleRigOverrideTarget");
        cmCamera.GetRig(2).LookAt = objectToView.Find("BottomRigOverrideTarget");
        AudioSource newSource;
        AudioClip clip;
        switch (objectToView.parent.GetComponent<PlayerStateController>().state)
        {
            case 0:
                newSource = GameObject.Find("IceSource").GetComponent<AudioSource>();
                clip = GameObject.Find("MusicStuff").GetComponent<AudioClipHolder>().clips[0];
                break;
            case 1:
                newSource = GameObject.Find("WaterSource").GetComponent<AudioSource>();
                clip = GameObject.Find("MusicStuff").GetComponent<AudioClipHolder>().clips[1];
                break;
            case 2:
                newSource = GameObject.Find("GasSource").GetComponent<AudioSource>();
                clip = GameObject.Find("MusicStuff").GetComponent<AudioClipHolder>().clips[2];
                break;
            default:
                newSource = GameObject.Find("IceSource").GetComponent<AudioSource>();
                clip = GameObject.Find("MusicStuff").GetComponent<AudioClipHolder>().clips[0];
                break;
        }
        handler.SwitchAudioTrackToVariant(old, clip, newVolume, newSource, true, 2.0f);
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
