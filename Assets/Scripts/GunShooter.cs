using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GunShooter : MonoBehaviour
{
    private GunSO gun;
    private Currency currency;
    [SerializeField] private MuzzleFlash muzzleFlash;
    
    public void Init(GunSO gun, Currency currency)
    {
        this.gun = gun;
        this.currency = currency;
    }

    public GunSO GetStats()
    {
        return gun;
    }
    public void Shoot()
    {
        muzzleFlash.PlayMuzzleFlash();
        currency.AddCurrency(gun.damage);
    }

    private void OnMouseDown()
    {
        Shoot();
    }

    private void Reset()
    {
        muzzleFlash = GetComponentInChildren<MuzzleFlash>();
    }
}
