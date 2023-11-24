using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class ShootingTarget : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] private GameObject bulletImpactPrefab;
    [SerializeField] private GameObject popUpTextPrefab;
    
    [SerializeField] private float bulletImpactLifeTime = 1f;
    [SerializeField] private float popUpTextLifeTime = 1f;

    ObjectPool<GameObject> bulletImpactPool;
    ObjectPool<GameObject> popUpTextPool;

    private void Start()
    {
        bulletImpactPool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnToPool, OnDestroyItem);
        popUpTextPool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnToPool, OnDestroyItem);
    }
    
    public void Hit(float damage)
    {
        animator.PlayInFixedTime("Hit", 0, 0);
        GameObject bulletImpactGO = bulletImpactPool.Get();
        bulletImpactGO.transform.position = transform.position;
        ReleasePoolItemWithDelay(bulletImpactPool, bulletImpactGO, bulletImpactLifeTime);
        
        GameObject popUpTextGO = popUpTextPool.Get();
        popUpTextGO.transform.position = transform.position;
        popUpTextGO.GetComponent<TMP_Text>().SetText("+" + damage.ToString("F0"));
        ReleasePoolItemWithDelay(popUpTextPool, popUpTextGO, popUpTextLifeTime);
    }
    
    private GameObject CreatePooledItem()
    {
        GameObject pooledItem = Instantiate(bulletImpactPrefab, transform.position, Quaternion.identity, transform);
        return pooledItem;
    }
    
    // Called when an item is taken from the pool using Get
    void OnTakeFromPool(GameObject item)
    {
        item.SetActive(true);
    }
    
    // Called when an item is returned to the pool using Release
    void OnReturnToPool(GameObject item)
    {
        item.SetActive(false);
    }
    
    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    void OnDestroyItem(GameObject item)
    {
        Destroy(item);
    }
    
    private async Task ReleasePoolItemWithDelay<T>(ObjectPool<T> pool, T item, float delay) where T : class
    {
        await Task.Delay(Mathf.RoundToInt(delay * 1000));
        pool.Release(item);
    }
    
    private void Reset()
    {
        animator = GetComponentInChildren<Animator>();
    }
}
