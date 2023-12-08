using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCellSpawner : MonoBehaviour
{
    [SerializeField] private GameObject gunCellPrefab;
    [SerializeField] private List<GunSO> guns;
    [SerializeField] private Currency currency;
    public event Action OnWin;
    
    private void Start()
    {
        foreach (var gun in guns)
        {
            GameObject gunCellGO = Instantiate(gunCellPrefab, transform);
            GunCell gunCell = gunCellGO.GetComponent<GunCell>();
            gunCell.Init(gun,currency);
            gunCell.OnUnlock += CheckWin;
        }
    }

    private void CheckWin()
    {
        foreach (var gun in guns)
        {
            if (!gun.isUnlocked)
                return;
        }
        OnWin?.Invoke();
    }
}
