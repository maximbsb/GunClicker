using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCellSpawner : MonoBehaviour
{
    [SerializeField] private List<GunSO> guns;
    
    private void Start()
    {
        foreach (var gun in guns)
        {
            GameObject gunCellGO = Instantiate(gun.prefab, transform);
            GunCell gunCell = gunCellGO.GetComponent<GunCell>();
            gunCell.Init(gun);
        }
    }
       
}
