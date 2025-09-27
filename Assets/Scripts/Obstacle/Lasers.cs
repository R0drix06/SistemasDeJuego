using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lasers : MonoBehaviour, IObstacle, IUpdatable
{
    public string id => "Laser";

    private BoxCollider2D boxCollider;
    private SpriteFlash spriteFlash;

    private Color startColor = Color.red;
    private Color flashColor = Color.blue;

    private float timer = 0;
    private float cooldown = 2;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = true;
        spriteFlash = GetComponent<SpriteFlash>();
        CustomUpdateManager.Instance.Register(this);
        IterationManager.Instance.updatables.Add(this);
    }

    public void Tick(float deltaTime)
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

}
