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
    [SerializeField] private Animator animator;
    
    
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
        currency.AddCurrency(gun.damage);
        animator.PlayInFixedTime("Shoot", 0, 0);
        if(muzzleFlash != null)
            muzzleFlash.PlayMuzzleFlash();
        else
            Debug.LogError("MuzzleFlash is null");
    }

    private void OnMouseDown()
    {
        Shoot();
    }

    private void Reset()
    {
        animator = GetComponentInChildren<Animator>();
        muzzleFlash = GetComponentInChildren<MuzzleFlash>();
    }
}
