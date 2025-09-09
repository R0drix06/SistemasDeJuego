using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lasers : MonoBehaviour, IObstacle
{
    public string id => "Laser";

    private BoxCollider2D boxCollider;
    private SpriteFlash spriteFlash;

    private Color startColor = Color.red;
    private Color flashColor = Color.blue;

    private float timer = 0;
    private float cooldown = 2;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = true;
        spriteFlash = GetComponent<SpriteFlash>();
    }

 
    public void Update()
    {
        Behaviour();
    }

    public void Behaviour()
    {
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            StartCoroutine(spriteFlash.FlashCoroutine(2, startColor, flashColor, 1));
            timer = 0;
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
