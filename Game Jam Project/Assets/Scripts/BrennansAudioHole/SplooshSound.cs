using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplooshSound : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> sounds;

    [SerializeField]
    private AudioSource movementSource;

    [SerializeField]
    private float volume;

    [SerializeField]
    private float timeBetweenPlays;

    private float timeSinceLastPlay;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastPlay = 0.0f;
        movementSource.volume = volume;
        movementSource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastPlay += Time.deltaTime;
        if (timeSinceLastPlay > timeBetweenPlays && GetComponent<PlayerMovement>().IsPlayerMoving())
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
