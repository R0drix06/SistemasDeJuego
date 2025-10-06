using Unity.VisualScripting;
using UnityEngine;

public abstract class LaserObstacle : ScriptableObject
{
    [SerializeField] protected Color startColor;
    [SerializeField] protected Color endColor;

    [SerializeField] protected float timer;
    [SerializeField] protected float cooldown = 2;

    public abstract void Behaviour();

}