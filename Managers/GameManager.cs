using Godot;
using Heimgaerd.Gui.Scripts;
using System;

public class GameManager : Node
{
    private ActorPlayer playerNode;
    private GUIManager guiNode;

    private bool isGamePaused;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        isGamePaused = false;
        playerNode= this.GetNode<ActorPlayer>("Player");
        guiNode = this.GetNode<GUIManager>("CanvasLayer/GUI");

        playerNode.Connect("UpdatedPlayerHp", guiNode, nameof(guiNode.UpdateHp));
        playerNode.Connect("UpdatedPlayerSp", guiNode, nameof(guiNode.UpdateSp));

        GD.Print("GameManager ready)");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
