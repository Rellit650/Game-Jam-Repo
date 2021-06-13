using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionMenuScript : MonoBehaviour
{
    public GameObject[] pageHolder;

    private int curPage = 0;
    public void NextPage() 
    {
        if (curPage < pageHolder.Length-1) 
        {
            pageHolder[curPage].SetActive(false);
            ++curPage;
            pageHolder[curPage].SetActive(true); 
        }
    }
    public void PreviousPage() 
    {
        if (curPage >= 1) 
        {
            pageHolder[curPage].SetActive(false);
            --curPage;
            pageHolder[curPage].SetActive(true);
        }
    }
}
