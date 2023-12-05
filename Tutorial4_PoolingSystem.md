# Pooling System
## In this tutorial we will learn how to optimise the instantiation of objects in our game by implementing a pooling system

A pooling system is a system that creates objects in advance and stores them in a pool (usually a `Stack`), rather than having them created and destroyed on demand, which is a more resource intensive process. It works by creating a set amount of GameObjects before the game starts and deactivates or activates the GameObjects in the pool, recycling the GameObject instead of destroying it. This system is especially useful when lot's of the same object has to be spawned such as bullets, particle systems or enemies.

One of the downsides of this system is that it uses more memory since the objects have to be stored there. That's why we can set a certain maximum quantity of objects that can be stored in the pool so that if we create a very large quantity of them, some of them will be deleted and some will be stored in the pool. We have to compromise between memory and the CPU workload.


## 1. Creating a pooling system
Unity has a special class that simplifies the management of an object pool called `ObjectPool`. This class is a stack that holds the collection of our instances that we can reuse.

This is an example of a pooling system script:
```.cs
public class PoolingSystem : MonoBehaviour
{
    private ObjectPool<GameObject> bulletImpactPool;
    private Vector3 spawnPosition;

    private void Start()
    {
        bulletImpactPool = new ObjectPool<GameObject>(() => CreatePooledItem(bulletImpactPrefab), OnTakeFromPool, OnReturnToPool, OnDestroyItem);
    }

     private GameObject CreatePooledItem(GameObject prefab)
    {
        GameObject pooledItem = Instantiate(prefab, transform.position, Quaternion.identity, transform.parent);
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

     public void OnHit(float damage)
    {
        GameObject bulletImpactGO = bulletImpactPool.Get();
        bulletImpactGO.transform.position = spawnPosition;
        ReleasePoolItemWithDelay(bulletImpactPool, bulletImpactGO, bulletImpactLifeTime);
    }
}
```
Now let's see what is happening here.
```.cs
bulletImpactPool = new ObjectPool<GameObject>(() => CreatePooledItem(bulletImpactPrefab), OnTakeFromPool, OnReturnToPool, OnDestroyItem);
```
Here we create a new pool. As you can see, we have 4 parameters that are of a type `Action` or `Func`.
The first parameter is createFunc: `() => CreatePooledItem(bulletImpactPrefab)`. This is going to be called when a pool has to instantiate a new instance of an object, which in our case is a `bulletImpactPrefab`.
The second parameter is actionGet: `OnTakeFromPool`. This is called when we already have an available deactivated instance of an object which we can take from the pool instead of instantiating/creating it.
The third parameter is actionOnRelease: `OnReturnToPool`. This is called when we want to return an object to the pool. All we do in the `OnReturnToPool` function is `SetActive(false);`, which deactivates the object.
The fourth parameter is `actionOnDestroy`: `OnDestroyItem`. This is called when an object has to be destroyed from the pool.

When our bullet hits the target, `OnHit` function is called. We call `bulletImpactPool.Get();` to either call `CreatePooledItem` or `OnTakeFromPool` depending on the circumstances. We save the object into a variable. Then we set a position for the bullet particle so that it gets played in the correct position;

The bullet impact then has to be put back into the pool. That's why the `ReleasePoolItemWithDelay` async function is called. In that function we call `pool.Release(item)`, which puts the object back in the pool and disables it after a certain number of miliseconds.

As you can see, particles appear when we shoot the gun:

https://github.com/maximbsb/GunClicker/assets/62714778/3e081013-f94d-4c88-88a2-00d8cc37f32d

Instead of being deleted, they just get turned off and when they are needed, we turn them back on:

![image](https://github.com/maximbsb/GunClicker/assets/62714778/1d677120-bc89-4288-9503-9c9b874f57d2)
