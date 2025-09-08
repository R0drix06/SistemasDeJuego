using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    [SerializeField] public GameObject[] obstacles;
    private Dictionary<string, GameObject> obstaclesDictionary;

    private void Awake()
    {
        obstaclesDictionary = new Dictionary<string, GameObject>();

        foreach (var gameObject in obstacles)
        {
            obstaclesDictionary.Add(gameObject.GetComponent<IObstacle>().id, gameObject);
        }
    }

    public GameObject Create(string name, Vector2 position, Quaternion rotation)
    {
        if (!obstaclesDictionary.TryGetValue(name, out GameObject obstacle))
        {
            Debug.Log("No se encontro obstaculo");
            return null;
        }
        
        return Instantiate(obstacle, position, rotation);
    }
}
