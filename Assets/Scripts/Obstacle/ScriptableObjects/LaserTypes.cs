using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New obstacle", menuName = "Obstacle/Lasers")]
public class LaserTypes : ScriptableObject
{
    [Header("Laser colors")]

    [SerializeField] public Color startColor;
    [SerializeField] public Color flashColor;

    [SerializeField] public float cooldown;

    [Header("Laser movement")]

    [SerializeField] public bool canMove;

    [SerializeField] public float moveSpeed;
    [SerializeField] public float moveCooldown;

}
