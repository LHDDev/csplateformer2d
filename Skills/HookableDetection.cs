using Godot;
using System.Collections.Generic;
using System.Linq ;

public class HookableDetection : Area2D
{
    private CollisionShape2D collisionShape2D;
    public List<HookSocle> accessibleHookSocles;
    private int selectedHookableIndex;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        collisionShape2D = this.FindChildrenOfType<CollisionShape2D>().ElementAt(0);

        GD.Print("test");
        accessibleHookSocles = new List<HookSocle>();
        selectedHookableIndex = 0;
    }

    /// <summary>
    /// Enable detection for Hookable spots
    /// </summary>
    /// <param name="isEnabled"> state if detection is enabled</param>
    public void EnableDetection(bool isEnabled)
    {
        collisionShape2D.Disabled = !isEnabled;
    }

    public void TryAddingHookable(HookSocle hookable)
    {
        // Is hookable accessible ?
        // YES => add to list
        // NO => do nothing

        accessibleHookSocles.Add(hookable);
        SortHookableByDistanceBeetweenPlayerAndHook();

        accessibleHookSocles.ForEach(hs => GD.Print($"Hook at {hs.Position}"));

    }

    private void SortHookableByDistanceBeetweenPlayerAndHook()
    {
        accessibleHookSocles.Sort();
    }

    public Vector2 getSelectedHookableGlobalPosition()
    {
        GD.Print("accessibleHokables :");
        accessibleHookSocles.ForEach(hs => GD.Print($"Hook at {hs.Position}"));
        return accessibleHookSocles.ElementAt<HookSocle>(selectedHookableIndex).GlobalPosition;
    }
}
