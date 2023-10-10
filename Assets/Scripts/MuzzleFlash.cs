using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] private List<GameObject> muzzleFlashes;
    private GameObject activatedMuzzleFlash;
    
    private void Start()
    {
        foreach (var muzzleFlash in muzzleFlashes)
        {
            muzzleFlash.SetActive(false);
        }
    }

    private void PlayMuzzleFlash()
    {
        StopAllCoroutines();
        if(activatedMuzzleFlash != null)
            activatedMuzzleFlash.SetActive(false);
        
        activatedMuzzleFlash = muzzleFlashes[Random.Range(0, muzzleFlashes.Count)];
        activatedMuzzleFlash.SetActive(true);
        StartCoroutine(StopMuzzleFlash());
    }
    
    private IEnumerator StopMuzzleFlash()
    {
        yield return new WaitForSeconds(0.15f);
        activatedMuzzleFlash.SetActive(false);
        activatedMuzzleFlash = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayMuzzleFlash();
        }
    }

    private void Reset()
    {
        muzzleFlashes = new List<GameObject>();
        foreach (Transform child in transform)
        {
            muzzleFlashes.Add(child.gameObject);
        }
    }
}
