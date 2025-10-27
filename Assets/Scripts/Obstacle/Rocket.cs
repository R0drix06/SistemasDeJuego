using UnityEngine;
using UnityEngine.Pool;

public class Rocket : MonoBehaviour, IObstacle, IUpdatable
{
    private Rigidbody2D rb;

    private RocketInitState initState;
    private RocketMiddleState middleState;
    private RocketFinalState finalState;

    private float speed;
    public float Speed { set => speed = value; }

    private float rotationSpeed;
    public float RotationSpeed { set => rotationSpeed = value; }

    private float buildUpCounter = 0;
    [SerializeField] private float buildUpSpeed;
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
        initState = new RocketInitState();
        middleState = new RocketMiddleState();
        finalState = new RocketFinalState();
    }

    public void Tick(float deltaTime)
    {
        Behaviour();
    }

    public void Behaviour()
    {
        BuildUpMethod();
        Vector2 direction = (target.transform.position - transform.position).normalized;
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotationSpeed;
        rb.linearVelocity = transform.up * speed;
    }

    private void BuildUpMethod()
    {
        buildUpCounter += Time.deltaTime;
        
        if (buildUpCounter >= buildUpSpeed)
        {
            finalState.ChangeRocketState(this);
        }
        else if (buildUpCounter >= buildUpSpeed/3)
        {
            middleState.ChangeRocketState(this);
        }
        else if (buildUpCounter >= 0)
        {
            initState.ChangeRocketState(this);
        }
    }

    public void Deactivate()
    {
        rb.linearVelocity = new Vector2 (0, 0);
        rb.angularVelocity = 0;
        buildUpCounter = 0;
        rocketPool.Release(gameObject);
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
