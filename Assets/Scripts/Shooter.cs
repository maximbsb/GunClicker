using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shooter : MonoBehaviour
{
    [FormerlySerializedAs("gun")] [SerializeField] private GunShooter gunShooter;
    [SerializeField] private Currency currency;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gunShooter.Shoot();
            currency.AddCurrency(gunShooter.GetStats().damage);
        }
    }

    private void Reset()
    {
        currency = FindObjectOfType<Currency>();
    }
}
