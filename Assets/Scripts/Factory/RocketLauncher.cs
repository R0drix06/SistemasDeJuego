using JetBrains.Annotations;
using UnityEngine;

public class RocketLauncher : MonoBehaviour, IUpdatable
{
    private ObstacleFactory obstacleFactory;

    [SerializeField] private float maxTime;
    private float currentTime = 0;

    private void Start()
    {
        obstacleFactory = GameObject.FindGameObjectWithTag("Factory").GetComponent<ObstacleFactory>();
        CustomUpdateManager.Instance.Register(this);
        IterationManager.Instance.updatables.Add(this);
    }

    public void Tick(float deltaTime)
    {
        currentTime += Time.deltaTime;

        if (currentTime > maxTime)
        {
            obstacleFactory.Create(obstacleFactory.obstacles[0].GetComponent<IObstacle>().id, transform.position, transform.rotation);
            currentTime = 0;
        }
    }
}
