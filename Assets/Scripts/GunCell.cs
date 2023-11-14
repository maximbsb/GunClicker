using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GunCell : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private TMP_Text gunNameText;
    [SerializeField] private TMP_Text gunDamageText;
    [SerializeField] private Transform gunTransform;
    public void Init(GunSO gunSO, Currency currency)
    {
        gunNameText.text = gunSO.name;
        gunDamageText.text = gunSO.damage.ToString("F0");
        
        GameObject gunGO = Instantiate(gunSO.prefab, gunTransform);
        if (gunGO.TryGetComponent(out GunShooter gunShooter))
        {
            gunShooter.Init(gunSO, currency);
        }
        else
        {
            Debug.LogError("Gun prefab does not have GunShooter component");
        }
    }
}
