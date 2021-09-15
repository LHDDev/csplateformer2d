using Godot;
using System;

public class PlayerRespawner : Node2D
{
    [Export]
    private NodePath respawnerAreaPath;

    private Area2D respawnerArea;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        respawnerArea = GetNode<Area2D>(respawnerAreaPath);
        respawnerArea.Connect("body_entered", this, nameof(RespawnerDetectBodies));
    }

    private void RespawnerDetectBodies(Node body)
    {
        if(body is ActorPlayer player)
        {
            player.SetRespawner(this);
        }
    }
}
