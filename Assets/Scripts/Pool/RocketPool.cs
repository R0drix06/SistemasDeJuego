using UnityEngine;
using UnityEngine.Pool;

public class RocketPool : MonoBehaviour
{
    [SerializeField] private GameObject rocket;

    private IObjectPool <GameObject> rocketPool;

    [SerializeField] private bool collectionCheck = true;

    private int defaultCapacity;
    private int maxCapacity;

    public int DefaultCapacity { set => defaultCapacity = value; }
    public int MaxCapacity { set => maxCapacity = value; }

    private void Awake()
    {
        rocketPool = new ObjectPool<GameObject>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPoolObject, collectionCheck, defaultCapacity, maxCapacity);
    
    }

    private GameObject CreateProjectile() //Functions as internal Awake()
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
        Rocket rocketToUnregister = poolObject.GetComponent<Rocket>();
        rocketToUnregister.OnDestroyCall();
        Destroy(poolObject.gameObject);
    }

    public void ShotObject(Vector2 transform, Quaternion rotation)
    {
        GameObject rocket = rocketPool.Get();

        if (rocket == null) return; //ThrowException (?) Sino, por qué tiraría null? No es posible sobrecargar los cohetes en el juego actual.

        rocket.transform.SetLocalPositionAndRotation(transform, rotation);
    }
}
