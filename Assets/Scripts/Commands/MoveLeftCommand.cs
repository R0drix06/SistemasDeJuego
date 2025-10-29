using UnityEngine;

public class MoveLeftCommand : ICommand
{
    private PlayerController player;

    public MoveLeftCommand(PlayerController playerController)
    {
        player = playerController;
    }

    public void Execute()
    {
        player.sr.flipX = true;
        if (player.rb2d.linearVelocityX > 0) //Si el jugador viene con inercia aplica más fricción.
        {
            player.rb2d.linearVelocity = new Vector2(player.rb2d.linearVelocity.x - player.MovementSpeed * player.TurningSpeed, player.rb2d.linearVelocity.y);
        }
        else //El jugador avanza
        {
            player.rb2d.linearVelocity = new Vector2(player.rb2d.linearVelocity.x - player.MovementSpeed, player.rb2d.linearVelocity.y);
        }
    }
}