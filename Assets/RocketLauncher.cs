using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    private ObstacleFactory obstacleFactory;

    private string name = "Rocket";

    private float currentTime = 0;

    private void Awake()
    {
        obstacleFactory = new ObstacleFactory();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > 5)
        {
            obstacleFactory.Create(name);
            currentTime = 0;
        }
    }
}
