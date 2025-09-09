using Unity.VisualScripting;
using UnityEngine;

public class Rocket : MonoBehaviour, IObstacle
{
    private Rigidbody2D rb;

    private float speed = 10f;
    private float rotationSpeed = 100f;
    private float rotateAmount;

    private GameObject target;

    public string id => "Rocket";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");

    }

    public void Update()
    {
        Behaviour();
    }

    public void Behaviour()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotationSpeed;
        rb.linearVelocity = transform.up * speed;
    }

    public void ResetLoop()
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
