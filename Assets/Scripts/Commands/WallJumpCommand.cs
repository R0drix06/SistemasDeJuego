using UnityEngine;

public class WallJumpCommand : ICommand
{
    private PlayerController player;
    public WallJumpCommand(PlayerController playerController)
    {
        player = playerController;
    }

    public void Execute()
    {
        if (player.MoveDirection > 0)
        {
            player.rb2d.linearVelocity = new Vector2(-player.WallJumpForce, player.JumpForce);
        }
        else if (player.MoveDirection < 0)
        {
            player.rb2d.linearVelocity = new Vector2(player.WallJumpForce, player.JumpForce);
        }
    }
}
