
using UnityEngine;

public interface IObstacle
{
    public string id { get; }

    public void Behaviour();

    public void ResetLoop();
}
