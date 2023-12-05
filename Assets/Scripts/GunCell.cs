using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GunCell : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private TMP_Text gunNameText;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private Button unlockButton;
    [SerializeField] private TMP_Text unlockButtonText;
    [SerializeField] private ShootingTarget shootingTarget;
    
    
    
    public void Init(GunSO gunSO, Currency currency)
    {
        gunNameText.text = gunSO.name;
        gunTransform.localScale = gunSO.scale * Vector3.one;
        unlockButtonText.text = $"Unlock: {gunSO.price}$";
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

        if (gunGO.TryGetComponent(out GunMaterialChanger gunMaterialChanger))
        {
            if (gunSO.isUnlocked)
            {
                gunMaterialChanger.SetUnlockedMaterial();
                animator.PlayInFixedTime("Unlocked", 0, 0);
                unlockButton.gameObject.SetActive(false);
            }
            else
            {
                gunTransform.GetComponentInChildren<Collider>().enabled = false;
                gunMaterialChanger.SetLockedMaterial();
                animator.PlayInFixedTime("Locked", 0, 0);
                unlockButton.onClick.AddListener(() =>
                {
                    if(currency.GetCurrency() < gunSO.price) return;
                    currency.SpendCurrency(gunSO.price);
                    gunSO.isUnlocked = true;
                    gunMaterialChanger.SetUnlockedMaterial();
                    animator.PlayInFixedTime("Unlock", 0, 0);
                    unlockButton.gameObject.SetActive(false);
                    gunTransform.GetComponentInChildren<Collider>().enabled = true;
                });
            }
        }
    }
    private void Reset()
    {
        gunNameText = GetComponentInChildren<TMP_Text>();
        shootingTarget = FindObjectOfType<ShootingTarget>();
        animator = GetComponent<Animator>();
        unlockButton = GetComponentInChildren<Button>();
        unlockButtonText = unlockButton.GetComponentInChildren<TMP_Text>();
    }
}
