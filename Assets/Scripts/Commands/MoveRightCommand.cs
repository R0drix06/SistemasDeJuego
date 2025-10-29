using UnityEngine;

public class MoveRightCommand : ICommand
{
    PlayerController player;

    public MoveRightCommand(PlayerController playerController)
    {
        player = playerController;
    }

    public void Execute()
    {
        player.sr.flipX = false;
        if (player.rb2d.linearVelocityX < 0) //Si el jugador viene con inercia aplica más fricción.
        {
            player.rb2d.linearVelocity = new Vector2(player.rb2d.linearVelocity.x + player.MovementSpeed * player.TurningSpeed, player.rb2d.linearVelocity.y);
        }
        else //El jugador avanza
        {
            player.rb2d.linearVelocity = new Vector2(player.rb2d.linearVelocity.x + player.MovementSpeed, player.rb2d.linearVelocity.y);
        }
    }
}
