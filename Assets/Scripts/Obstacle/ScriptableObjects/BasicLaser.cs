
using UnityEngine;

[CreateAssetMenu(fileName = "New Laser", menuName = "Laser/BasicLaser")]
internal class BasicLaser : LaserObstacle
{
    private float currentMoveX = 0;
    private float currentMoveY = 0;
    private bool moveUp = true;
    private bool moveRight = true;

    public Transform transform;
    [SerializeField] private float DistanceX;
    [SerializeField] private float DistanceY;
    [SerializeField] private bool canMoveInX;
    [SerializeField] private bool canMoveInY;

    public override void Behaviour()
    {
        if (canMoveInX)
        {
            if (currentMoveX < DistanceX && moveUp)
            {
                currentMoveX += Time.deltaTime;
                transform.Translate(Vector3.right);

                if (currentMoveX > DistanceX)
                {
                    moveUp = false;
                }
            }
            else if (currentMoveX > 0 && !moveUp)
            {
                currentMoveX -= Time.deltaTime;
                transform.Translate(Vector3.left);

                if (currentMoveX < 0)
                {
                    moveUp = true;
                }
            }

            Debug.Log(currentMoveX);
        }
        if (canMoveInY)
        {
            if (currentMoveX < DistanceY && moveRight)
            {
                currentMoveY += Time.deltaTime;
                transform.Translate(Vector3.up);

                if(currentMoveY > DistanceY)
                {
                    moveRight = false;
                }
            }
            else if (currentMoveX > 0 && !moveRight)
            {
                currentMoveY -= Time.deltaTime;
                transform.Translate(Vector3.down);

                if (currentMoveY < 0)
                {
                    moveRight = true;
                }
            }

            Debug.Log(currentMoveY);
        }
    }

}
