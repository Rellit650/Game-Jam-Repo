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

    bool moved = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
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
                    if (!moved) 
                    {
                        StartCoroutine(moveIt());
                        moved = true;
                    }
                }
            }
        }
    }

    IEnumerator moveIt() 
    {
        yield return new WaitForSeconds(0.01f);
        moveMe.transform.position = Vector3.MoveTowards(moveMe.transform.position, target.position, 0.01f);
        if ((moveMe.transform.position - target.position).sqrMagnitude > 0)
        {
            StartCoroutine(moveIt());
        }
    }
}
