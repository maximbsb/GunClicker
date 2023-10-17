using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunStats stats;
    [SerializeField] private MuzzleFlash muzzleFlash;

    public GunStats GetStats()
    {
        return stats;
    }
    public void Shoot()
    {
        muzzleFlash.PlayMuzzleFlash();
    }

    private void Reset()
    {
        muzzleFlash = GetComponentInChildren<MuzzleFlash>();
    }
}
