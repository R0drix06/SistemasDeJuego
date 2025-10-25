using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private BouncingScript bouncingScript;

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
            IterationManager.Instance.NextLevel();
        }
    }
}
