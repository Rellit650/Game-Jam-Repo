using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public void FindOptionsMenu()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
        GameObject.Find("DontDestroyOnLoadCanvas").GetComponent<Canvas>().enabled = true;
    }
    public void CloseOptionsMenu()
    {
        GameObject.Find("DontDestroyOnLoadCanvas").GetComponent<Canvas>().enabled = false;
    }
}
