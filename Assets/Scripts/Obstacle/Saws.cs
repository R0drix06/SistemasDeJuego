using UnityEngine;
using UnityEngine.SceneManagement;

public class Saws : MonoBehaviour, IObstacle, IUpdatable
{
    public string id => "Saw";

    [SerializeField] private float rotSpeed;

    private void Start()
    {
        CustomUpdateManager.Instance.Register(this);
        IterationManager.Instance.updatables.Add(this);
    }

    public void Tick(float deltaTime)
    {
        Behaviour();
    }

    public void Behaviour()
    {
        transform.Rotate(0,0,rotSpeed * Time.deltaTime);
    }



}
