using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCell : MonoBehaviour
{
    private GunSO gunSO;
    private GunShooter gunShooter;
    private Currency currency;

    public void Init(GunSO gunSO)
    {
        this.gunSO = gunSO;
        gunShooter.Init(gunSO, currency);
    }

    public void Buy()
    {
        if (currency.GetCurrency() >= gunSO.damage)
        {
            currency.AddCurrency(-gunSO.damage);
            gunShooter.Shoot();
        }
    }

    private void Reset()
    {
        gunShooter = GetComponent<GunShooter>();
        currency = FindObjectOfType<Currency>();
    }
}
