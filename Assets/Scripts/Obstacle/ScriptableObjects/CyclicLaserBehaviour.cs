using UnityEngine;

[CreateAssetMenu(fileName = "New Laser", menuName = "Laser/CyclicLaser")]
internal class CyclicLaserBehaviour : LaserObstacle
{
    public BoxCollider2D boxCollider;
    public SpriteFlash spriteFlash;

    public override void Behaviour()
    {
    }
}
