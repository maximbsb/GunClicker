using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Gun gun;
    [SerializeField] private Currency currency;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gun.Shoot();
            currency.AddCurrency(gun.GetStats().damage);
        }
    }

    private void Reset()
    {
        currency = FindObjectOfType<Currency>();
    }
}
