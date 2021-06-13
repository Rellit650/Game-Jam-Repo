using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZoneScript : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private int splitsRequired;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (other.transform.parent.GetComponent<PlayerSplitController>().splitsLeft >= splitsRequired) 
            {
                SceneManager.LoadScene(sceneName);
            }           
        }
    }
}
