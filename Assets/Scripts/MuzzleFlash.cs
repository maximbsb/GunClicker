using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] private List<Animator> muzzleFlashes;
    private Animator activatedMuzzleFlash;
    
    private void Start()
    {
        foreach (var muzzleFlash in muzzleFlashes)
        {
            muzzleFlash.gameObject.SetActive(false);
        }
    }

    public void PlayMuzzleFlash()
    {
        StopAllCoroutines();
        if(activatedMuzzleFlash != null)
            activatedMuzzleFlash.gameObject.SetActive(false);
        
        activatedMuzzleFlash = muzzleFlashes[Random.Range(0, muzzleFlashes.Count)];
        activatedMuzzleFlash.gameObject.SetActive(true);
        StartCoroutine(StopMuzzleFlash());
    }
    
    private IEnumerator StopMuzzleFlash()
    {
        float waitTime = activatedMuzzleFlash.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(waitTime);
        activatedMuzzleFlash.gameObject.SetActive(false);
        activatedMuzzleFlash = null;
    }
    
    private void Reset()
    {
        muzzleFlashes = new List<Animator>();
        foreach (Transform child in transform)
        {
            muzzleFlashes.Add(child.GetComponent<Animator>());
        }
    }
}
