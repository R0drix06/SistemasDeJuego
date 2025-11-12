using UnityEngine;
using UnityEngine.Pool;

public class Rocket : MonoBehaviour, IObstacle, IUpdatable
{
    private Rigidbody2D rb;

    #region States
    private RocketInitState initState;
    private RocketMiddleState middleState;
    private RocketFinalState finalState;
    #endregion

    #region Movement Variables
    private float speed;
    private float rotationSpeed;
    public float Speed { set => speed = value; }
    public float RotationSpeed { set => rotationSpeed = value; }
    #endregion

    #region State Changers
    private float buildUpCounter = 0;
    [SerializeField] private float buildUpSpeed;
    private bool isReleased = false;
    #endregion

    private GameObject target;

    private IObjectPool <GameObject> rocketPool;
    public IObjectPool<GameObject> RocketPool { set =>  rocketPool = value; }

    public string id => "Rocket";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        initState = new RocketInitState();
        middleState = new RocketMiddleState();
        finalState = new RocketFinalState();
        CustomUpdateManager.Instance.Register(this);
        IterationManager.Instance.updatables.Add(this);
    }

    private void OnDestroy()
    {
        CustomUpdateManager.Instance.Unregister(this);
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
        else if (buildUpCounter >= buildUpSpeed/2)
        {
            middleState.ChangeRocketState(this);
        }
        else if (buildUpCounter >= 0)
        {
            initState.ChangeRocketState(this);
        }
    }

    public void ResetState()
    {
        isReleased = false;
        buildUpCounter = 0;
    }

    public void Deactivate()
    {
        if (isReleased) return;

        isReleased = true;
        rocketPool.Release(gameObject);
    }

    public void OnDestroyCall()
    {
        CustomUpdateManager.Instance.Unregister(this);
        IterationManager.Instance.updatables.Remove(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Deactivate();
    }

}
