using UnityEngine;
using UnityEngine.SceneManagement;

public class CyclicLaser : MonoBehaviour ,IObstacle
{
    public string id => "CyclicLaser";

    private float currentTime = 0;
    [SerializeField] public float switchTime;

    private SpriteRenderer sprite;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color deactiveColor;

    private bool active = false;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

    }
  
    public void Update()
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

    public void ResetLoop()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController controller))
        {
            if (controller.invincibility == false) IterationManager.Instance.ResetLevel();
        }
    }

}
