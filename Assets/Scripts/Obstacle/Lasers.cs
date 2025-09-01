using UnityEngine;
using UnityEngine.SceneManagement;

public class Lasers : MonoBehaviour, IObstacle
{
    public void Behaviour()
    {
        throw new System.NotImplementedException();
    }

    public void ResetLoop()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
