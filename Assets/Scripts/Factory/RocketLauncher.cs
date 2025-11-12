using UnityEngine;

public class RocketLauncher : MonoBehaviour, IUpdatable
{
    private RocketPool rocketPool;

    [SerializeField] private float maxTime;
    private float currentTime = 0;

    [SerializeField] private int defaultCapacity;
    [SerializeField] private int maxCapacity;

    private void Start()
    {
        rocketPool = GameObject.FindGameObjectWithTag("Factory").GetComponent<RocketPool>();
        rocketPool.DefaultCapacity = defaultCapacity;
        rocketPool.MaxCapacity = maxCapacity;
        CustomUpdateManager.Instance.Register(this);
        IterationManager.Instance.updatables.Add(this);
    }

    private void OnDestroy()
    {
        CustomUpdateManager.Instance.Unregister(this);
    }

    public void Tick(float deltaTime)
    {
        currentTime += Time.deltaTime;

        if (currentTime > maxTime)
        {
            rocketPool.ShootObject(transform.position, transform.rotation);
            currentTime = 0;
        }
    }
}
