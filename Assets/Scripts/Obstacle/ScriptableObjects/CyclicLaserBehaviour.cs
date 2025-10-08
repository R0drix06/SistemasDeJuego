using UnityEngine;

[CreateAssetMenu(fileName = "New Laser", menuName = "Laser/CyclicLaser")]
internal class CyclicLaserBehaviour : LaserObstacle
{
    private bool active = false;

    public override void Behaviour()
    {
        if (!active)
        {
            timer += Time.deltaTime;

            if (timer > cooldown)
            {
                active = true;
                boxCollider.enabled = true;
                sprite.color = startColor;
            }
        }
        else
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                active = false;
                boxCollider.enabled = false;
                sprite.color = endColor;
            }
        }
    }
}
