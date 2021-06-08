using Godot;
using System;
using System.Linq;
using TestCs.Core.Hook;

public class Hookable : HookableBase
{
    private Area2D hookReception;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        hookReception = this.FindChildrenOfType<Area2D>().ElementAt(0);
        hookReception.Connect("area_entered", this, nameof(onAreaEntered));
        hookReception.AddToGroup("hookable_group");
    }


    private void onAreaEntered(Area2D area)
    {
        if(area is HookableDetection hookableDetection)
        {
            DistFromPlayer = this.GlobalPosition.DistanceTo(area.GlobalPosition);
            hookableDetection.TryAddingHookable(this);
        }
    }

}
