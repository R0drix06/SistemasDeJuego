using UnityEngine;
using UnityEngine.EventSystems;
public class JumpCommand : ICommand
{
    private PlayerController player;

    public JumpCommand(PlayerController playerController)
    {
        player = playerController;
    }

    public void Execute()
    {
        player.rb2d.linearVelocity = new Vector2(player.rb2d.linearVelocity.x, player.JumpForce); //Se aplica un impulso vertical
    }

    
}
