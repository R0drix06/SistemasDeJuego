using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    SpriteRenderer sr;

    private float moveDirection;
    
    [Header("Movement Speed")]
    [SerializeField] private float baseMaxSpeed = 25f;
    [SerializeField] private float movementSpeed = 20f;
    private float currentMaxSpeed = 25f;

    [Header("Jump")]
    [SerializeField] private bouncingScript bouncer;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float bufferTime = 1f;
    private float currentCoyoteTime = 0;
    private float currentBufferTime = 0;
    private bool jumpBuffered = false;

    private bool wallJumpAvailable = false;

    [Header("Dash")]
    [SerializeField] private float dashForce = 30f;
    [SerializeField] private float dashMaxTime = 0.2f;
    private float currentDashTime = 0;
    private bool dashActive = false;
    
    [Header("Friction")]
    [SerializeField] private float turningSpeed = 3f;
    [SerializeField] private float walkingFriction = 0.9f;

    private bool playerGrounded = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        PlayerMovement();

        PlayerSpeedCap();

        InputBuffer();
    }

    private void FixedUpdate()
    {
        Dash();

        PlayerPhysics();
    }

    private void PlayerMovement()
    {
        moveDirection = Input.GetAxisRaw("Horizontal"); //hacia donde se mueve el jugador (1 = Derecha, -1 = Izquierda).

        if (bouncer.playerBouncing)
        {
            rb2d.linearVelocity = new Vector2(0, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            jumpBuffered = true;
            currentBufferTime = bufferTime; //Reseteo el timer del buffer.
        }

        if (Input.GetKeyUp(KeyCode.K) && rb2d.linearVelocity.y > 0)
        {
            rb2d.linearVelocityY = rb2d.linearVelocity.y * 0.35f; //Si se deja de presionar el botón de salto, el impulso vertical disminuye.
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            dashActive = true;
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
            rb2d.linearVelocity = new Vector2(-jumpForce, jumpForce);
        }
        else if (moveDirection < 0)
        {
            rb2d.linearVelocity = new Vector2(jumpForce, jumpForce);
        }
    }

    private void Dash()
    {
        if (dashActive)
        {
            currentDashTime += Time.deltaTime;
            currentMaxSpeed = dashForce; //Aumenta la velocidad máxima.

            if (sr.flipX) //Aplica una fuerza horizontal dependiendo de la dirección del jugador.
            {
                rb2d.linearVelocity = new Vector2(-dashForce, 0);
            }
            else
            {
                rb2d.linearVelocity = new Vector2(dashForce, 0);
            }

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

        if (currentDashTime == dashMaxTime)
        {
            rb2d.linearVelocity = new Vector2(0, 0);
        }
    }

    private void PlayerPhysics()
    {
        if (moveDirection > 0)
        {
            if (rb2d.linearVelocityX < 0) //Si el jugador viene con inercia aplica más fricción.
            {
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x + movementSpeed * turningSpeed, rb2d.linearVelocity.y);
            }
            else //El jugador avanza
            {
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x + movementSpeed, rb2d.linearVelocity.y);
            }
        }
        else if (moveDirection < 0)
        {
            if (rb2d.linearVelocityX > 0) //Si el jugador viene con inercia aplica más fricción.
            {
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x - movementSpeed * turningSpeed, rb2d.linearVelocity.y);
            }
            else //El jugador avanza
            {
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x - movementSpeed, rb2d.linearVelocity.y);
            }
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


    //COLLISSIONS

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            playerGrounded = true;
        }

        if (collision.collider.CompareTag("Wall"))
        {
            wallJumpAvailable = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            playerGrounded = false;
        }

        if (collision.collider.CompareTag("Wall"))
        {
            wallJumpAvailable = false;
        }
    }
}
