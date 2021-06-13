using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class DataHandler : MonoBehaviour
{
    public static DataHandler instance;

    private bool invertX = true;
    private bool invertY = false;
    private float sensX = 225f;
    private float sensY = 1.5f;

    [SerializeField]
    private Toggle UIInvertX;
    [SerializeField]
    private Toggle UIInvertY;
    [SerializeField]
    private Slider UISensitivityX;
    [SerializeField]
    private Slider UISensitivityY;
    // Start is called before the first frame update

    private void OnEnable()
    {
        UIInvertX.isOn = invertX;
        UIInvertY.isOn = invertY;
        UISensitivityX.value = sensX;
        UISensitivityY.value = sensY;
    }
    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject.transform.parent.gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
    public void ApplySettings() 
    {
        invertX = UIInvertX.isOn;
        invertY = UIInvertY.isOn;
        sensX = UISensitivityX.value;
        sensY = UISensitivityY.value;
    }

    public void LoadSettings(CinemachineFreeLook cmCamera) 
    {
        cmCamera.m_XAxis.m_InvertInput = invertX;
        cmCamera.m_YAxis.m_InvertInput = invertY;

        cmCamera.m_XAxis.m_MaxSpeed = sensX;
        cmCamera.m_YAxis.m_MaxSpeed = sensY;
    }
}
