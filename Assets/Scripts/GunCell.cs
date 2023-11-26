using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GunCell : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private TMP_Text gunNameText;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private ShootingTarget shootingTarget;
    
    public void Init(GunSO gunSO, Currency currency)
    {
        gunNameText.text = gunSO.name;
        gunTransform.localScale = gunSO.scale * Vector3.one;
        GameObject gunGO = Instantiate(gunSO.prefab, gunTransform);
        if (gunGO.TryGetComponent(out GunShooter gunShooter))
        {
            gunShooter.Init(gunSO, currency);
            gunShooter.OnShoot += shootingTarget.Hit;
        }
        else
        {
            Debug.LogError("Gun prefab does not have GunShooter component");
        }
    }
}
