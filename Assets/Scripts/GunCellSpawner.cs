using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCellSpawner : MonoBehaviour
{
    [SerializeField] private GameObject gunCellPrefab;
    [SerializeField] private List<GunSO> guns;
    [SerializeField] private Currency currency;
    
    private void Start()
    {
        foreach (var gun in guns)
        {
            GameObject gunCellGO = Instantiate(gunCellPrefab, transform);
            GunCell gunCell = gunCellGO.GetComponent<GunCell>();
            gunCell.Init(gun,currency);
        }
    }
}
