using UnityEngine;

public class Rocket : Obstacle
{
    private Rigidbody2D rb;
    private float speed = 10;

    public override string Name => "Rocket";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Behaviour();
    }

    public override void Behaviour()
    {
        speed += speed * 2f * Time.deltaTime;
        Vector2 moveDirection = transform.up * speed;
        rb.linearVelocity = moveDirection;
    }

    public override void ResetLoop()
    {
        IterationManager.Instance.ResetLevel();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResetLoop();
        }
    }
}
