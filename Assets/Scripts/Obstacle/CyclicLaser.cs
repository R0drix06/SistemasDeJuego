using UnityEngine;
using UnityEngine.SceneManagement;

public class CyclicLaser : MonoBehaviour ,IObstacle, IUpdatable
{
    public string id => "CyclicLaser";

    private float currentTime = 0;
    [SerializeField] public float switchTime;

    private SpriteRenderer sprite;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color deactiveColor;

    private bool active = false;

    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        CustomUpdateManager.Instance.Register(this);

    }

    public void Tick(float deltaTime)
    {
        Behaviour();
    }

    public void Behaviour()
    {
        if (!active)
        {
            currentTime += Time.deltaTime;

            if (currentTime > switchTime)
            {
                active = true;
                boxCollider.enabled = true;
                sprite.color = activeColor;
            }
        }
        else
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0)
            {
                active = false;
                boxCollider.enabled = false;
                sprite.color = deactiveColor;
            }
        }
    }
}
