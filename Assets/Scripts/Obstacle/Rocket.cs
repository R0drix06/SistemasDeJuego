using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour, IObstacle
{
    private Rigidbody2D rb;
    private int speed = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Behaviour()
    {
        rb.linearVelocityX = speed * Time.deltaTime;
    }

    public void ResetLoop()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        Behaviour();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ResetLoop();
        }
    }
}
