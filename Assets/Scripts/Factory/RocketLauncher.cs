using JetBrains.Annotations;
using UnityEngine;

public class RocketLauncher : MonoBehaviour, IUpdatable
{
    //private ObstacleFactory obstacleFactory;

    private RocketPool rocketPool;

    [SerializeField] private float maxTime;
    private float currentTime = 0;

    [SerializeField] private int defaultCapacity;
    [SerializeField] private int maxCapacity;

    private void Start()
    {
        //obstacleFactory = GameObject.FindGameObjectWithTag("Factory").GetComponent<ObstacleFactory>();

        rocketPool = GameObject.FindGameObjectWithTag("Factory").GetComponent<RocketPool>();
        rocketPool.DefaultCapacity = defaultCapacity;
        rocketPool.MaxCapacity = maxCapacity;
        CustomUpdateManager.Instance.Register(this);
        IterationManager.Instance.updatables.Add(this);
    }

    public void Tick(float deltaTime)
    {
        currentTime += Time.deltaTime;

        if (currentTime > maxTime)
        {
            //obstacleFactory.Create(obstacleFactory.obstacles[0].GetComponent<IObstacle>().id, transform.position, transform.rotation);

            rocketPool.ShotObject(transform.position, transform.rotation);
            currentTime = 0;
        }
    }
}
