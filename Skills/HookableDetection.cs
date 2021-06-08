using Godot;
using System.Collections.Generic;
using System.Linq ;
using TestCs.Core.Hook;

public class HookableDetection : Area2D
{
    private CollisionShape2D collisionShape2D;
    public List<HookableBase> accessibleHookSocles;
    private int selectedHookableIndex;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        collisionShape2D = this.FindChildrenOfType<CollisionShape2D>().ElementAt(0);

        accessibleHookSocles = new List<HookableBase>();
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

    public void TryAddingHookable(HookableBase hookable)
    {
        // Is hookable accessible ?
        // YES => add to list
        // NO => do nothing

        accessibleHookSocles.Add(hookable);
        accessibleHookSocles.Sort();


    }

    public Vector2 getSelectedHookableGlobalPosition()
    {
        return accessibleHookSocles.ElementAt<HookableBase>(selectedHookableIndex).GlobalPosition;
    }
}
