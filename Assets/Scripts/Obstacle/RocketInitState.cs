public class RocketInitState : IRocketStates
{
    private float speed => 10f;
    private float rotationSpeed => 100f;

    public void ChangeRocketState(Rocket rocket)
    {
        rocket.Speed = speed;
        rocket.RotationSpeed = rotationSpeed;
    }
}
