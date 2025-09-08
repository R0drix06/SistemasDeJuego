using Unity.VisualScripting;
using UnityEngine;

public class Rocket : Obstacle
{
    private Rigidbody2D rb;

    private float speed = 10f;
    private float rotationSpeed = 100f;
    private float rotateAmount;

    private GameObject target;

    public override string id => "Rocket";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Behaviour();
    }

    public override void Behaviour()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotationSpeed;
        rb.linearVelocity = transform.up * speed;
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
        else if (collision.gameObject)
        {
            Destroy(gameObject);
        }
    }


}
