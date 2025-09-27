using UnityEngine;
using UnityEngine.SceneManagement;

public class Saws : MonoBehaviour, IObstacle, IUpdatable
{
    public string id => "Saw";

    private Rigidbody2D rb;
    [SerializeField] private int speed;

    [SerializeField] private Transform down;

    public LayerMask frontLayer;
    public LayerMask downLayer;

    [SerializeField] private float distFront;
    [SerializeField] private float distDown;

    private bool infoFront;
    private bool infoDown;

    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        CustomUpdateManager.Instance.Register(this);
        IterationManager.Instance.updatables.Add(this);

    }

    public void Tick(float deltaTime)
    {
        Behaviour();
    }

    public void Behaviour()
    {
        rb.linearVelocity = transform.right * speed;

        infoFront = Physics2D.Raycast(transform.position, transform.right, distFront, frontLayer);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + transform.right * distFront);
        Gizmos.DrawLine(down.position, down.position + transform.up * -1 * distDown);
    }


}
