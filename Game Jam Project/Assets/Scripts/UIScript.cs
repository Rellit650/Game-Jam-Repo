using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public List<Image> splitImages;

    [SerializeField]
    Sprite solidSprite;
    [SerializeField]
    Sprite liquidSprite;
    [SerializeField]
    Sprite gasSprite;
    [SerializeField]
    Sprite Cube;
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

    public void RemoveUISplit(int index) 
    {
        splitImages[index].sprite = Cube;
    }

}
