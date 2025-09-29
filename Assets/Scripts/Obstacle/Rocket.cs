using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Progress;

public class Rocket : MonoBehaviour, IObstacle, IUpdatable
{
    private Rigidbody2D rb;

    private float speed = 10f;
    private float rotationSpeed = 100f;
    private float rotateAmount;

    private GameObject target;

    private IObjectPool <GameObject> rocketPool;
    public IObjectPool<GameObject> RocketPool { set =>  rocketPool = value; }

    public string id => "Rocket";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        CustomUpdateManager.Instance.Register(this);
        IterationManager.Instance.updatables.Add(this);
    }

    public void Tick(float deltaTime)
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

    public void Deactivate()
    {
        rb.linearVelocity = new Vector2 (0, 0);
        rb.angularVelocity = 0;
        rocketPool.Release(this.gameObject);
    }

    public void OnDestroyCall()
    {
        CustomUpdateManager.Instance.Unregister(this);
        IterationManager.Instance.updatables.Remove(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            Deactivate();
        }
    }

}
