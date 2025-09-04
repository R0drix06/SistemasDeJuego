using UnityEngine;
using UnityEngine.SceneManagement;

public class Saws : Obstacle
{
    public override string Name => "Saw";

    private Rigidbody2D rb;
    private int speed;

    public override void Behaviour()
    {
        throw new System.NotImplementedException();
    }

    public override void ResetLoop()
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
