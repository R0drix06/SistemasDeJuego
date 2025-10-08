
using UnityEngine;

[CreateAssetMenu(fileName = "New Laser", menuName = "Laser/BasicLaser")]
internal class BasicLaser : LaserObstacle
{

    public override void Behaviour()
    {
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            Flash(2, startColor, endColor, 1);
            timer = 0;
        }
    }

    private void Flash(float flashDuration, Color startColor, Color flashColor, int flashesAmount)
    {

        float elapsedFlashTime = 0;
        float elapsedFlashPercentage = 0;

        while (elapsedFlashTime < flashDuration)
        {
            elapsedFlashTime += Time.deltaTime;
            elapsedFlashPercentage = elapsedFlashTime / flashDuration;

            if (elapsedFlashPercentage > 1)
            {
                elapsedFlashPercentage = 1;
            }

            float pingPongPercentage = Mathf.PingPong(elapsedFlashPercentage * 2 * flashesAmount, 1);
            sprite.color = Color.LerpUnclamped(startColor, flashColor, pingPongPercentage);

        }
    }
}
