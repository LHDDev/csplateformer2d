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
    }

    /// <summary>
    /// Enable detection for Hookable spots
    /// </summary>
    /// <param name="isEnabled"> state if detection is enabled</param>
    public void EnableDetection()
    {
        selectedHookableIndex = 0;
        collisionShape2D.Disabled = false;
    }

    public void DisableDetection()
    {
        accessibleHookSocles.ElementAt<HookableBase>(selectedHookableIndex).SetSelected(false);
        collisionShape2D.Disabled = true;
    }
    public void TryAddingHookable(HookableBase hookable)
    {
        // Is hookable accessible ?
        // YES => add to list
        // NO => do nothing

        accessibleHookSocles.Add(hookable);
        accessibleHookSocles.Sort();

        RefreshShaders();

    }

    public Vector2 GetSelectedHookableGlobalPosition()
    {
        GD.Print($"selected Index = {selectedHookableIndex}");
        accessibleHookSocles.ElementAt<HookableBase>(selectedHookableIndex).SetSelected(false);
        return accessibleHookSocles.ElementAt<HookableBase>(selectedHookableIndex).GlobalPosition;
    }

    public void SelectNextHookable()
    {
        if (selectedHookableIndex >= accessibleHookSocles.Count - 1)
        {
            selectedHookableIndex = 0;
        }
        else
            selectedHookableIndex ++;
        RefreshShaders();
    }

    public void SelectPreviousHookable()
    {
        if (selectedHookableIndex <= 0)
        {
            selectedHookableIndex = accessibleHookSocles.Count - 1;
        }
        else
            selectedHookableIndex --;

        RefreshShaders();
    }

    private void RefreshShaders()
    {
        accessibleHookSocles.ForEach(h => h.SetSelected(false));
        accessibleHookSocles.ElementAt<HookableBase>(selectedHookableIndex).SetSelected(true);

    }
}
