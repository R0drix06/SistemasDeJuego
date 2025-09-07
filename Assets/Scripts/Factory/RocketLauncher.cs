using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    private ObstacleFactory obstacleFactory;

    private float currentTime = 0;

    private void Awake()
    {
        obstacleFactory = GetComponent<ObstacleFactory>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > 0.5f)
        {
            obstacleFactory.Create(obstacleFactory.obstacles[0].GetComponent<Obstacle>().id, transform.position, transform.rotation);
            currentTime = 0;
        }
    }
}
