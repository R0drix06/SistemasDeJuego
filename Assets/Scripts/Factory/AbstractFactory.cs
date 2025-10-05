using UnityEngine;

public class AbstractFactory : MonoBehaviour
{
    private ObstacleFactory obstacleFactory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        obstacleFactory = GetComponent<ObstacleFactory>();
    }

    public GameObject CallObstacleFactory(string name)
    {
        return obstacleFactory.Create(name);
    }
}
