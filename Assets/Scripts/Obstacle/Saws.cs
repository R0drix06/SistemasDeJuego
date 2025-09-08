using UnityEngine;
using UnityEngine.SceneManagement;

public class Saws : Obstacle
{
    public override string id => "Saw";

    private Rigidbody2D rb;
    [SerializeField] private int speed;

    [SerializeField] private Transform front;
    [SerializeField] private Transform down;

    public LayerMask frontLayer;
    public LayerMask downLayer;

    [SerializeField] private float distFront;
    [SerializeField] private float distDown;

    private bool infoFront;
    private bool infoDown;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    public override void Behaviour()
    {
        rb.linearVelocity = transform.right * speed;

        infoFront = Physics2D.Raycast(front.position, transform.right, distFront, frontLayer);
        infoDown = Physics2D.Raycast(down.position, transform.up * -1, distDown, downLayer);

        if (infoFront)
        {
            transform.Rotate(0f, 0f, 90f);
        }
        if (!infoDown)
        {
            transform.Rotate(0f, 0f, -90f);
        }
    }

    void Update()
    {
      Behaviour();  
    }

    public override void ResetLoop()
    {
        IterationManager.Instance.ResetLevel();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            ResetLoop();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(front.position, front.position + transform.right * distFront);
        Gizmos.DrawLine(down.position, down.position + transform.up * -1 * distDown);
    }
}
