using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "New Gun", order = 0)]
public class GunSO : ScriptableObject
{
    public string name;
    public GameObject prefab;
    public float scale = 750;
    public float damage;
    public float fireRate = 0.5f;
    public bool isUnlocked;
    public int price;
}
