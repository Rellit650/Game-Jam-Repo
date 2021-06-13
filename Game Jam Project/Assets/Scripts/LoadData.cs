using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LoadData : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {
        CinemachineFreeLook cmCamera = gameObject.GetComponent<CinemachineFreeLook>();
        DataHandler.instance.LoadSettings(cmCamera);
    }
}
