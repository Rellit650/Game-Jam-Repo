using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public List<Image> splitImages;

    public List<Image> outlineImages;

    public Image PlayerImage;
    public TextMeshProUGUI SplitCounterText;
    public GameObject pauseMenu;

    [SerializeField]
    Sprite solidSprite;
    [SerializeField]
    Sprite liquidSprite;
    [SerializeField]
    Sprite gasSprite;
    [SerializeField]
    Sprite Cube;

    private int curOutline = 3;

    public int SetSplitUI(int state) 
    {
        int index = -1;
        for (int i = 0; i < splitImages.Count; i++) 
        {
            if (splitImages[i].sprite == Cube) 
            {
                index = i;
                break;
            }
        }

        if (index == -1) 
        {
            Debug.LogWarning("No Images avaible in splitImages. splitholder & splitImages are out of alignment!");
            return index;
        }

        splitImages[index].sprite = state switch
        {
            0 => solidSprite,
            1 => liquidSprite,
            2 => gasSprite,
            -1 => null,
            _ => null
        };

        return index;
        /*
        if (state == 0)
        {
            splitImages[index] = solidSprite;
        }
        else if (state == 1) 
        {
            splitImages[index] = liquidSprite;
        }
        else if (state == 2)
        {
            splitImages[index] = gasSprite;
        }
        else if (state == -1)
        {
            splitImages[index] = Cube;
        }
        */
    }

    public void ActivateOutline(int indexToActivate) 
    {
        outlineImages[curOutline].gameObject.GetComponentInChildren<Image>().enabled = false;
        outlineImages[indexToActivate].gameObject.GetComponentInChildren<Image>().enabled = true;
        curOutline = indexToActivate;
    }

    public void UpdateSplitIcon(int index, int state) 
    {
        splitImages[index].sprite = state switch
        {
            0 => solidSprite,
            1 => liquidSprite,
            2 => gasSprite,
            -1 => null,
            _ => null
        };
    }

    public void SetPlayerImage(int state) 
    {
        PlayerImage.sprite = state switch
        {
            0 => solidSprite,
            1 => liquidSprite,
            2 => gasSprite,
            -1 => null,
            _ => null
        };
    }

    public void SetSplitCounter(int counter) 
    {
        SplitCounterText.text = "Splits Left: " + counter + "/3"; 
    }
    public void RemoveUISplit(int index) 
    {
        splitImages[index].sprite = Cube;
    }


    //Pause menu functions
    public void PullUpPauseMenu() 
    {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RestartLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu() 
    {
        SceneManager.LoadScene("StartScreen");
    }
}
