public class RocketFinalState : IRocketStates
{
    private float speed = 30f;
    private float rotationSpeed = 25f;

    public void ChangeRocketState(Rocket rocket)
    {
        rocket.Speed = speed;
        rocket.RotationSpeed = rotationSpeed;
    }
}
