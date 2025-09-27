using UnityEngine;
using UnityEngine.Pool;

public class RocketPool : MonoBehaviour
{
    [SerializeField] private GameObject rocket;

    private IObjectPool <GameObject> rocketPool;

    [SerializeField] private bool collectionCheck = true;

    [SerializeField] private int defaultCapaity = 20;
    [SerializeField] private int maxCapaity = 100;

    private void Awake()
    {
        rocketPool = new ObjectPool<GameObject>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPoolObject, collectionCheck, defaultCapaity, maxCapaity);
    }

    private GameObject CreateProjectile()
    {
        GameObject projectile = Instantiate(rocket);
        projectile.GetComponent<Rocket>().RocketPool = rocketPool;
        return projectile;
    }

    private void OnGetFromPool (GameObject poolObject)
    {
        poolObject.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(GameObject poolObject)
    {
        poolObject.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject poolObject)
    {
        Destroy(poolObject.gameObject);
    }

}
