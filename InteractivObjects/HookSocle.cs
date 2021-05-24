using Godot;
using System;
using System.Linq;

public class HookSocle : Position2D, IComparable<HookSocle>
{
    private Area2D hookReception;
    public float distFromPlayer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        hookReception = this.FindChildrenOfType<Area2D>().ElementAt(0);
        hookReception.Connect("area_entered", this, nameof(onAreaEntered));
    }


    private void onAreaEntered(Area2D area)
    {
        if(area is HookableDetection hookableDetection)
        {
            GD.Print("Here !");
            distFromPlayer = this.Position.DistanceTo(area.Position);
            hookableDetection.TryAddingHookable(this);
        }
    }

    public int CompareTo(HookSocle other)
    {
        int indexOrder = distFromPlayer.CompareTo(other.distFromPlayer);

        if ( indexOrder != 0)
        {
            return indexOrder;
        }

        return 0;
    }
}
