using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    [SerializeField] private Obstacle[] obstacles;
    private Dictionary<string, Obstacle> obstaclesDictionary;

    private void Awake()
    {
        obstaclesDictionary = new Dictionary<string, Obstacle>();

        foreach (var obstacle in obstacles)
        {
            obstaclesDictionary.Add(obstacle.Name, obstacle);
        }
    }

    public Obstacle Create(string name)
    {
        if (!obstaclesDictionary.TryGetValue(name, out Obstacle obstacle))
        {
            Debug.Log("No se encontro obstaculo");
            return null;
        }
        
        return Instantiate(obstacle);
    }
}
