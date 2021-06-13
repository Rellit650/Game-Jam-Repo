using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTransitionScript : MonoBehaviour
{
    public Transform target;
    public Transform start;
    public float duration = 0f;
    public float transitionTime = 0.5f;


    //private Transform playerTarget;
    private CinemachineFreeLook cmCamera;

    private void Start()
    {
        cmCamera = FindObjectOfType<CinemachineFreeLook>();
    }

    // Update is called once per frame

    private void Update()
    {
        if (duration >= transitionTime)
        {
            cmCamera.LookAt = target;
            cmCamera.Follow = target;
            this.enabled = false;
        }
    }
    void FixedUpdate()
    {
        duration += Time.deltaTime;
        gameObject.transform.position = Vector3.Lerp(start.position, target.position, duration / transitionTime);       
    }
    public void SetTarget(Transform newTarget) 
    {
        if (duration < transitionTime)
        {
            duration *= 0.5f;
        }
        else 
        {
            duration = 0f;
        }
        start = gameObject.transform;
        target = newTarget;
    }
}
