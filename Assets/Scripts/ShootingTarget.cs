using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using Random = System.Random;

public class ShootingTarget : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] private GameObject bulletImpactPrefab;
    [SerializeField] private GameObject popUpTextPrefab;
    
    [SerializeField] private float bulletImpactLifeTime = 1f;
    [SerializeField] private float popUpTextLifeTime = 1f;

    [SerializeField] private Vector2 targetSize = new Vector2(2,2);
    [SerializeField] private Transform targetCenter;
    [SerializeField] private float popUpTextOffset = 1f;

    

    ObjectPool<GameObject> bulletImpactPool;
    ObjectPool<GameObject> popUpTextPool;
    private Vector3 randomTargetPosition;

    private void Start()
    {
        bulletImpactPool = new ObjectPool<GameObject>(() => CreatePooledItem(bulletImpactPrefab), OnTakeFromPool, OnReturnToPool, OnDestroyItem);
        popUpTextPool = new ObjectPool<GameObject>(() => CreatePooledItem(popUpTextPrefab), OnTakeFromPool, OnReturnToPool, OnDestroyItem);
    }
    
    public void Hit(float damage)
    {
        animator.PlayInFixedTime("Hit", 0, 0);
        GameObject bulletImpactGO = bulletImpactPool.Get();
        randomTargetPosition = targetCenter.position + UnityEngine.Random.Range(-targetSize.x/2, targetSize.x/2) * targetCenter.right + UnityEngine.Random.Range(-targetSize.y/2, targetSize.y/2) * Vector3.up;
        bulletImpactGO.transform.position = randomTargetPosition;
        bulletImpactGO.transform.rotation = Quaternion.LookRotation(targetCenter.forward);
        ReleasePoolItemWithDelay(bulletImpactPool, bulletImpactGO, bulletImpactLifeTime);
        
        GameObject popUpTextGO = popUpTextPool.Get();
        PopUp popUp = popUpTextGO.GetComponent<PopUp>();
        popUp.Init(damage);
        popUpTextGO.transform.position = randomTargetPosition + targetCenter.forward * popUpTextOffset;
        ReleasePoolItemWithDelay(popUpTextPool, popUpTextGO, popUpTextLifeTime);
    }
    
    private GameObject CreatePooledItem(GameObject prefab)
    {
        GameObject pooledItem = Instantiate(prefab, transform.position, Quaternion.identity);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(targetCenter.position,targetSize.x * targetCenter.right + targetSize.y * Vector3.up );
        Gizmos.DrawLine(targetCenter.position, targetCenter.position + targetCenter.forward * popUpTextOffset);
    }
}
