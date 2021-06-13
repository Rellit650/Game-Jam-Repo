using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonScript : MonoBehaviour
{
    public void ExitMenu() 
    {
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
        GameObject.Find("DontDestroyOnLoadCanvas").GetComponent<Canvas>().enabled = false;
    }
}
