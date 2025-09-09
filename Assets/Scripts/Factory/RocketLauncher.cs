using JetBrains.Annotations;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    private ObstacleFactory obstacleFactory;

    [SerializeField] private float maxTime;
    private float currentTime = 0;

    private void Awake()
    {
        obstacleFactory = GetComponent<ObstacleFactory>();
    }

    public void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > maxTime)
        {
            obstacleFactory.Create(obstacleFactory.obstacles[0].GetComponent<IObstacle>().id, transform.position, transform.rotation);
            currentTime = 0;
        }
    }
}
