using Unity.VisualScripting;
using UnityEngine;

public class LaserConfigs : MonoBehaviour, IUpdatable
{
    [SerializeField] private LaserTypes laser;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private SpriteFlash spriteFlash;

    private Color startColor;
    private Color flashColor;

    private float timer;
    private float cooldown;

    #region LaserMovement

    private bool moveRight = true;
    private bool canMove;

    private float moveTimer;
    private float moveSpeed;
    private float moveCooldown;

    #endregion



    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = true;
        spriteFlash = GetComponent<SpriteFlash>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        startColor = laser.startColor;
        flashColor = laser.flashColor;
        cooldown = laser.cooldown;
        spriteRenderer.color = startColor;
        
        canMove = laser.canMove;
        moveSpeed = laser.moveSpeed;
        moveCooldown = laser.moveCooldown;

        CustomUpdateManager.Instance.Register(this);
        IterationManager.Instance.updatables.Add(this);
    }

    public void Tick(float deltaTime)
    {
        Behaviour();
        Flash();
    }

    public void Behaviour()
    {
        if (canMove)
        {
            if (moveRight)
            {
                moveTimer += Time.deltaTime;
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);

                if (moveTimer > moveCooldown)
                {
                    moveRight = false;
                }
            }
            else
            {
                moveTimer -= Time.deltaTime;
                transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);

                if (moveTimer < 0)
                {
                    moveRight = true;
                }
            }
        }

    }

    private void Flash()
    {
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            StartCoroutine(spriteFlash.FlashCoroutine(2, startColor, flashColor, 1));
            timer = 0;
        }
    }
}
