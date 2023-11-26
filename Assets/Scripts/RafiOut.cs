using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RafiOut : MonoBehaviour
{
    public int rafi = 0;
    
    public int RafiNotOut()
    {
        return 100;
    }
    
    public void RafiOut1(out int rafiInt)
    {
        rafiInt = 100;
    }

    private void Start()
    {
        rafi = RafiNotOut();
        Debug.Log(rafi);
        
        RafiOut1(out rafi);
        Debug.Log(rafi);
    }
}
