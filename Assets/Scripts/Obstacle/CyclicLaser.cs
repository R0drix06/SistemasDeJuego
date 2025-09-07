using UnityEngine;
using UnityEngine.SceneManagement;

public class CyclicLaser : Obstacle
{
    public override string id => "CyclicLaser";

    private float currentTime = 0;
    [SerializeField] public float switchTime;

    private SpriteRenderer sprite;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color deactiveColor;

    private bool active = false;

    private BoxCollider2D boxCollider;

    public override void Behaviour()
    {
        throw new System.NotImplementedException();
    }

    public override void ResetLoop()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController controller))
        {
            if (controller.invincibility == false) IterationManager.Instance.ResetLevel();
        }
    }
}
