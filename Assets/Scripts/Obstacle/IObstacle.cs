
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public abstract string Name { get; }

    public abstract void Behaviour();

    public abstract void ResetLoop();
}
