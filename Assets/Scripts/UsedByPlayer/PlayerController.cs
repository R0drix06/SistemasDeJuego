using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IUpdatable 
{
    #region Commands
    ICommand moveRight;
    ICommand moveLeft;
    ICommand moveDash;

    #endregion

    public Rigidbody2D rb2d;
    public SpriteRenderer sr;

    private float moveDirection;
    
    [Header("Movement Speed")]
    [SerializeField] private float baseMaxSpeed;
    [SerializeField] private float movementSpeed;
    private float currentMaxSpeed;
    
    public float MovementSpeed => movementSpeed;

    [Header("Dash")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashMaxTime;
    private float currentDashTime = 0;
    private bool dashActive = false;

    public float DashForce => dashForce;

    [Header("Friction")]
    [SerializeField] private float turningSpeed;
    [SerializeField] private float walkingFriction;

    public float TurningSpeed => turningSpeed;
    public float WalkingFriction => walkingFriction;

    [Header("Jump")]
    [SerializeField] private BouncingScript bouncer;
    [SerializeField] private float jumpForce;
    [SerializeField] private float wallJumpForce;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float bufferTime;
    private float currentCoyoteTime = 0;
    private float currentBufferTime = 0;
    public bool jumpBuffered = false;

    private bool wallJumpAvailable = false;
    public bool WallJumpAvailable { get { return wallJumpAvailable; } set { wallJumpAvailable = value; } }


    [Header("Invincibility")]
    [SerializeField] private float invincibilityDuration;

    private bool invincibility = false;
    public bool Inivincibility { get { return invincibility; } }


    [Header("Collision")]
    private bool playerGrounded = false;
    public bool PlayerGrounded { get { return playerGrounded; } set { playerGrounded = value; } }

    

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        moveRight = new MoveRightCommand(this);
        moveLeft = new MoveLeftCommand(this);
        moveDash = new DashCommand(this);
        CustomUpdateManager.Instance.Register(this);
    }

    public void Tick(float deltaTime)
    {
        PlayerInput();

        PlayerSpeedCap();

        InputBuffer();
    }

    private void FixedUpdate()
    {
        Dash();

        PlayerMovement();
    }

    private void PlayerInput()
    {
        moveDirection = Input.GetAxisRaw("Horizontal"); //hacia donde se mueve el jugador (1 = Derecha, -1 = Izquierda).

        if (bouncer.playerBouncing)
        {
            rb2d.linearVelocity = new Vector2(0, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            jumpBuffered = true;
            currentBufferTime = bufferTime; //Reseteo el timer del buffer.
        }

        if (Input.GetKeyUp(KeyCode.X) && rb2d.linearVelocity.y > 0)
        {
            rb2d.linearVelocityY = rb2d.linearVelocity.y * 0.35f; //Si se deja de presionar el botón de salto, el impulso vertical disminuye.
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            dashActive = true;
            StartCoroutine(StartInvincibility());
        }

        //Rotacion del sprite.
        if (moveDirection > 0)
        {
            sr.flipX = false;
        }
        else if (moveDirection < 0)
        {
            sr.flipX = true;
        }
    }

    private void Jump()
    {
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce); //Se aplica un impulso vertical
    }

    private void WallJump()
    {
        //Se aplica un impulso vertical y un impulso horizontal en dirección contraria al movimiento del jugador.
        if (moveDirection > 0)
        {
            rb2d.linearVelocity = new Vector2(-wallJumpForce, jumpForce);
        }
        else if (moveDirection < 0)
        {
            rb2d.linearVelocity = new Vector2(wallJumpForce, jumpForce);
        }
    }

    private void Dash()
    {
        if (dashActive)
        {
            currentDashTime += Time.deltaTime;
            currentMaxSpeed = dashForce; //Aumenta la velocidad máxima.

            moveDash.Execute();

        }
        else
        {
            currentMaxSpeed = baseMaxSpeed; //resetea la velocidad máxima.
        }
        
        if (currentDashTime > dashMaxTime) //Desactiva el dash al terminar el timer
        {
            currentDashTime = 0;
            dashActive = false;
        }
    }

    private void PlayerMovement()
    {
        if (moveDirection > 0)
        {
            moveRight.Execute();
        }
        else if (moveDirection < 0)
        {
            moveLeft.Execute();
        }
        else //Si el jugador no se mueve el personaje desacelera lentamente.
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x * walkingFriction, rb2d.linearVelocity.y);
        }
    }

    private void PlayerSpeedCap()
    {
        if (rb2d.linearVelocityX >= currentMaxSpeed)
        {
            rb2d.linearVelocityX = currentMaxSpeed;
        }
        else if (rb2d.linearVelocityX <= -currentMaxSpeed)
        {
            rb2d.linearVelocityX = -currentMaxSpeed;
        }
    }

    private void InputBuffer()
    {
        // Coyote time
        if (playerGrounded)
        {
            currentCoyoteTime = coyoteTime; //Reseteo el Timer al tocar el suelo.
        }
        else
        {
            currentCoyoteTime -= Time.deltaTime; //Empiezo el timer al dejar el suelo.
        }

        // Input Buffer
        if (currentBufferTime > 0)
        {
            currentBufferTime -= Time.deltaTime;
        }
        else
        {
            jumpBuffered = false;
        }

        if (jumpBuffered)
        {
            if (playerGrounded || currentCoyoteTime > 0f)
            {
                Jump();
                jumpBuffered = false;
            }
            else if (wallJumpAvailable)
            {
                WallJump();
                jumpBuffered = false;
            }
        }
    }
    private IEnumerator StartInvincibility()
    {
        invincibility = true;
        yield return new WaitForSeconds(invincibilityDuration);
        invincibility = false;
    }
   
}
