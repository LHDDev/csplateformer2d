using Godot;
using Heimgaerd.Core.Hook;
using System;
using System.Collections.Generic;
using System.Linq;

public class HookableDetection : Area2D
{

    private CollisionShape2D collisionShape2D;
    public List<HookableBase> accessibleHookSocles;
    private int selectedHookableIndex;

    private Godot.Collections.Array exceptions;
    private Physics2DDirectSpaceState spaceState;

    // Debug
    private List<Vector2> targetPosition;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        collisionShape2D = this.FindChildrenOfType<CollisionShape2D>().ElementAt(0);
        accessibleHookSocles = new List<HookableBase>();
        spaceState = GetWorld2d().DirectSpaceState;
        exceptions = new Godot.Collections.Array();
        exceptions.Add(this);
        exceptions.Add(GetParent());
        targetPosition = new List<Vector2>();
    }

    public override void _Process(float delta)
    {
        Update();
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
        if(accessibleHookSocles.Count > 0)
        {
            accessibleHookSocles.ElementAt<HookableBase>(selectedHookableIndex).SetSelected(false);
        }
        collisionShape2D.Disabled = true;
        targetPosition.Clear();

    }
    public void TryAddingHookable(HookableBase hookable)
    {
        Godot.Collections.Dictionary result = spaceState.IntersectRay(this.GlobalPosition, hookable.GlobalPosition,exceptions,CollisionMask,true,true);

        targetPosition.Add((Vector2)result["position"]);
        if(((Node2D)result["collider"] ).GetParent() is HookableBase) // Horrible, a refaire avec les collisions names
        {
            accessibleHookSocles.Add(hookable);
            accessibleHookSocles.Sort();
            RefreshShaders();
        }

    }

    public Vector2 GetSelectedHookableGlobalPosition()
    {
        GD.Print($"selected Index = {selectedHookableIndex}");
        accessibleHookSocles.ElementAt<HookableBase>(selectedHookableIndex).SetSelected(false);
        return accessibleHookSocles.ElementAt<HookableBase>(selectedHookableIndex).GlobalPosition;
    }

    public void SelectNextHookable()
    {
        if(accessibleHookSocles.Count > 0)
        {
            if (selectedHookableIndex >= accessibleHookSocles.Count - 1)
            {
                selectedHookableIndex = 0;
            }
            else
                selectedHookableIndex ++;
            RefreshShaders();
        }
    }

    public void SelectPreviousHookable()
    {
        if(accessibleHookSocles.Count > 0)
        {
            if (selectedHookableIndex <= 0)
            {
                selectedHookableIndex = accessibleHookSocles.Count - 1;
            }
            else
                selectedHookableIndex --;

            RefreshShaders();
        }
    }

    public override void _Draw()
    {
        foreach(Vector2 tposition in targetPosition)
        {
            DrawLine(this.Position, tposition- this.GlobalPosition, new Color(1, 0, 0), 1);
            DrawCircle(tposition - this.GlobalPosition, 2, new Color(1, 0, 0));
        }
    }

    private void RefreshShaders()
    {
        accessibleHookSocles.ForEach(h => h.SetSelected(false));
        accessibleHookSocles.ElementAt<HookableBase>(selectedHookableIndex).SetSelected(true);
    }
}
