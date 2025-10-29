using UnityEngine;

public class DashCommand : ICommand
{
    PlayerController player;
    public DashCommand(PlayerController playerController)
    {
        player = playerController;
    }

    public void Execute()
    {
        if (player.sr.flipX) //Aplica una fuerza horizontal dependiendo de la dirección del jugador.
        {
            player.rb2d.linearVelocity = new Vector2(-player.DashForce, 0);
        }
        else
        {
            player.rb2d.linearVelocity = new Vector2(player.DashForce, 0);
        }
    }
}
