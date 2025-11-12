using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private BouncingScript bouncingScript;

    [SerializeField] private GameObject xInputPrompt;
    [SerializeField] private GameObject lrInputPrompt;
    [SerializeField] private GameObject cInputPrompt;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        bouncingScript = GetComponentInChildren<BouncingScript>();
    }

    private void Unregister()
    {
        CustomUpdateManager.Instance.Unregister(playerController);
        CustomUpdateManager.Instance.Unregister(bouncingScript);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            playerController.PlayerGrounded = true;
        }

        if (collision.collider.CompareTag("Wall"))
        {
            playerController.WallJumpAvailable = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            playerController.PlayerGrounded = false;
        }

        if (collision.collider.CompareTag("Wall"))
        {
            playerController.WallJumpAvailable = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Rocket"))
        {
            Unregister();
            IterationManager.Instance.ResetLevel();
        }

        if (collision.collider.CompareTag("Spikes"))
        {
            Unregister();
            IterationManager.Instance.ResetLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laser") && playerController.Inivincibility == false)
        {
            Unregister();
            IterationManager.Instance.ResetLevel();
        }
        if (collision.gameObject.CompareTag("Saw"))
        {
            Unregister();
            IterationManager.Instance.ResetLevel();
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            Unregister();
            IterationManager.Instance.NextLoop();
        }

        if (collision.gameObject.CompareTag("LevelGoal"))
        {
            Unregister();
            IterationManager.Instance.NextLevel();
        }

        if (collision.gameObject.CompareTag("X_Prompt"))
        {
            xInputPrompt?.SetActive(true);
        }

        if (collision.gameObject.CompareTag("LR_Prompt"))
        {
            lrInputPrompt?.SetActive(true);
        }

        if (collision.gameObject.CompareTag("C_Prompt"))
        {
            cInputPrompt?.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("X_Prompt"))
        {
            xInputPrompt?.SetActive(false);
        }

        if (collision.gameObject.CompareTag("LR_Prompt"))
        {
            lrInputPrompt?.SetActive(false);
        }

        if (collision.gameObject.CompareTag("C_Prompt"))
        {
            cInputPrompt?.SetActive(false);
        }
    }
}
