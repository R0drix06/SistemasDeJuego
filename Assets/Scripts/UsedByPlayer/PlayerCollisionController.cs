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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            AudioManager.Instance.PlaySFXSound(playerController.landSFX, playerController.transform, 0.1f);
        }

        if (collision.collider.CompareTag("Rocket"))
        {
            IterationManager.Instance.ResetLevel();
        }

        if (collision.collider.CompareTag("Spikes"))
        {
            IterationManager.Instance.ResetLevel();
        }
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
            playerController.currentCoyoteTime = playerController.coyoteTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Saw"))
        {
            IterationManager.Instance.ResetLevel();
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            IterationManager.Instance.NextLoop();
        }

        if (collision.gameObject.CompareTag("LevelGoal"))
        {
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laser") && playerController.Inivincibility == false)
        {
            IterationManager.Instance.ResetLevel();
        }
    }
}
