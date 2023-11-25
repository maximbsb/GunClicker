using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private TMP_Text tmpText;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        Vector3 lookRotation = transform.position - mainCamera.transform.position;
        transform.rotation = Quaternion.LookRotation(lookRotation);
    }
    
    public void Init(float value)
    {
        tmpText.text = ConvertValueToString(value);
    }

    private string ConvertValueToString(float value)
    {
        if(value >= 1000000000)
            return "+" + (value / 1000000000).ToString("F2") + "B";
        if(value >= 1000000)
            return "+" + (value / 1000000).ToString("F2") + "M";
        if(value >= 1000)
            return "+" + (value / 1000).ToString("F2") + "K";
        
        return "+" + value.ToString("F0");
    }
}
