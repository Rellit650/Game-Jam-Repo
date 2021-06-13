using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplooshSound : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> sounds;
    
    private AudioSource movementSource;

    [SerializeField]
    private float volume;

    [SerializeField]
    private float timeBetweenPlays;

    private float timeSinceLastPlay;

    private bool sploosh;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastPlay = 0.0f;
        movementSource = GameObject.Find("Sploosh").GetComponent<AudioSource>();
        movementSource.volume = volume;
        movementSource.loop = false;
        sploosh = GetComponent<PlayerMovement>().enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (!sploosh)
        {
            sploosh = GetComponent<PlayerMovement>().enabled;
        }

        timeSinceLastPlay += Time.deltaTime;
        if (timeSinceLastPlay > timeBetweenPlays && GetComponent<PlayerMovement>().IsPlayerMoving() && sploosh)
        {
            switch ((int)(Random.value * 3))
            {
                case 0:
                    //Debug.Log("0");
                    movementSource.PlayOneShot(sounds[0]);
                    break;
                case 1:
                    //Debug.Log("1");
                    movementSource.PlayOneShot(sounds[1]);
                    break;
                case 2:
                    //Debug.Log("2");
                    movementSource.PlayOneShot(sounds[2]);
                    break;
            }

            timeSinceLastPlay = 0.0f;
        }
    }
}
