using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    public enum activationType 
    {
        Move,
        Destroy,
    }

    public activationType purpose;
    [SerializeField]
    private GameObject blowMeUp;
    [SerializeField]
    private GameObject moveMe;
    [SerializeField]
    private Transform target;

    private Vector3 startPosition;
    private Vector3 endTarget;

    bool moved = false;

    private void Start()
    {
        if(purpose == activationType.Move)
        {
            startPosition = moveMe.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Split")) 
        {
            if (other.transform.parent.GetComponent<PlayerStateController>().state == 0) 
            {
                Debug.Log("Pressure Plate active");

                if (purpose == activationType.Destroy) 
                {
                    Destroy(blowMeUp);
                }
                if (purpose == activationType.Move) 
                {
                    endTarget = target.position;
                    moveMe.GetComponent<AudioSource>().Play();
                    StartCoroutine(moveIt());
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Split"))
        {
            Debug.Log(purpose);
    
            if (purpose == activationType.Move)
            {
                endTarget = startPosition;
                moveMe.GetComponent<AudioSource>().Play();
                StartCoroutine(moveIt());
            }
        }
    }

    IEnumerator moveIt() 
    {
        while ((moveMe.transform.position - endTarget).sqrMagnitude > 0)
        {
            moveMe.transform.position = Vector3.MoveTowards(moveMe.transform.position, endTarget, 0.01f);
            yield return null;
        }
        moveMe.GetComponent<AudioSource>().Stop();
    }
}
