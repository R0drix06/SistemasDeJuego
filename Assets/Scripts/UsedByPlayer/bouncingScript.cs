using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BouncingScript : MonoBehaviour, IUpdatable
{
    LightOverchargeCommand lightOverchargeCommand;
    [SerializeField] private Light2D overchargeLight;

    [SerializeField] private float bounceTime = 0.2f;
    private float currentTime = 0;

    public bool playerBouncing = false;

    private PlayerController playerController;

    private void Start()
    {
        CustomUpdateManager.Instance.Register(this);
        playerController = GetComponentInParent<PlayerController>();
        lightOverchargeCommand = new LightOverchargeCommand(overchargeLight);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnDestroy()
    {
        CustomUpdateManager.Instance.Unregister(this);
    }

    public void Tick(float deltaTime)
    {
        currentTime += Time.deltaTime;

        if (currentTime >= bounceTime) //Cuando termina el timer se desactiva.
        {

            lightOverchargeCommand.Execute(false);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.X)) //Activo el sprite renderer y collider, reseteo el timer.
        {
            if (!playerController.PlayerGrounded)
            {
                lightOverchargeCommand.Execute(true);
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                currentTime = 0;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bounce"))
        {
            playerBouncing = true;
            currentTime = 0.2f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bounce"))
        {
            playerBouncing = false;
        }
    }

   
}
