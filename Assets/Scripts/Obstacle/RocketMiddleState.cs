
using UnityEditor.Rendering;

public class RocketMiddleState : IRocketStates
{
    private float speed = 7f;
    private float rotationSpeed = 100f;
    public void ChangeRocketState(Rocket rocket)
    {
        rocket.Speed = speed;
        rocket.RotationSpeed = rotationSpeed;
    }
}

