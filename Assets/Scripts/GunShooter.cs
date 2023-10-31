using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GunShooter : MonoBehaviour
{
    [FormerlySerializedAs("so")] [FormerlySerializedAs("stats")] [SerializeField] private GunSO gun;
    [SerializeField] private MuzzleFlash muzzleFlash;
    private Currency currency;
    public void Init(GunSO gun)
    {
        this.gun = gun;
    }

    public GunSO GetStats()
    {
        return gun;
    }
    public void Shoot()
    {
        muzzleFlash.PlayMuzzleFlash();
    }

    private void OnMouseDown()
    {
        Shoot();
        currency.AddCurrency(gun.damage);
    }

    private void Reset()
    {
        muzzleFlash = GetComponentInChildren<MuzzleFlash>();
    }
}
