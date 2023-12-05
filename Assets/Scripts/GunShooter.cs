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
    public event Action<float> OnShoot;
    private float timeSinceLastShot = 0;
    private bool isHolding;

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
        OnShoot?.Invoke(gun.damage);
        currency.AddCurrency(gun.damage);
        animator.PlayInFixedTime("Shoot", 0, 0);

        if (muzzleFlash != null)
            muzzleFlash.PlayMuzzleFlash();
        else
            Debug.LogError("MuzzleFlash is null");

    }

    private void OnMouseDown()
    {
        Shoot();
        isHolding = true;
    }

    private void Update()
    {
        if (isHolding)
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= gun.fireRate)
            {
                Shoot();
                timeSinceLastShot = 0;
            }
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            timeSinceLastShot = 0;
            isHolding = false;
        }
    }
    
    private void Reset()
    {
        animator = GetComponentInChildren<Animator>();
        muzzleFlash = GetComponentInChildren<MuzzleFlash>();
    }
}
