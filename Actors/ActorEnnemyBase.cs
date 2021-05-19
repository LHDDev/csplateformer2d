using Godot;
using System;

public class ActorEnnemyBase : ActorBase
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }
    public override void UpdateHP(int amount)
    {
        CurrentHealt -= amount;
    }

    public override void UpdateSP(int amount)
    {
        CurrentHealt -= amount;
    }

}
