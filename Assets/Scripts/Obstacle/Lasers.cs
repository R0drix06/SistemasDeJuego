using UnityEngine;
using UnityEngine.SceneManagement;

public class Lasers : Obstacle
{
    public override string id => "Laser";

    private BoxCollider2D boxCollider;

    public override void Behaviour()
    {
        throw new System.NotImplementedException();
    }

    public override void ResetLoop()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController controller))
        {
            if (controller.invincibility == false) IterationManager.Instance.ResetLevel();
        } 
    }
}
