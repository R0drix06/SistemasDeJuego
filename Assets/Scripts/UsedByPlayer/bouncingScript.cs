using UnityEngine;

public class BouncingScript : MonoBehaviour, IUpdatable
{
    [SerializeField] private float bounceTime = 0.2f;
    private float currentTime = 0;

    public bool playerBouncing = false;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        CustomUpdateManager.Instance.Register(this);
    }


    public void Tick(float deltaTime)
    {
        currentTime += Time.deltaTime;

        if (currentTime >= bounceTime) //Cuando termina el timer se desactiva.
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.X)) //Activo el sprite renderer y collider, reseteo el timer.
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            currentTime = 0;
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
