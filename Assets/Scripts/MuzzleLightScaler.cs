using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MuzzleLightScaler : MonoBehaviour
{
    [SerializeField] private float defaultRange = 15f;
    [SerializeField] private Light muzzleLight;
    private void Reset()
    {
        muzzleLight = GetComponent<Light>();
    }

    private void Awake()
    {
        muzzleLight.range = 0;
    }

    public async void ChangeSize(float waitTime)
    {
        float t = 0;
        while (t < waitTime)
        {
            t += Time.deltaTime;
            muzzleLight.range = Mathf.Lerp(defaultRange, 0, t / waitTime);
            await Task.Yield();
        }
    }
}
