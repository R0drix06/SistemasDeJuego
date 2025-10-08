using UnityEngine;

public class LaserBehaviour : MonoBehaviour, IUpdatable
{
    [SerializeField] private LaserObstacle laser;

    void Start()
    {
        laser.sprite = GetComponent<SpriteRenderer>();
        laser.boxCollider = GetComponent<BoxCollider2D>();

        CustomUpdateManager.Instance.Register(this);
        IterationManager.Instance.updatables.Add(this);
    }

    public void Tick(float deltaTime)
    {
        laser.Behaviour();
    }
}
