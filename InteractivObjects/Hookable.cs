using Godot;
using Heimgaerd.Core.Hook;
using System;
using System.Linq;

public class Hookable : HookableBase
{
    [Export]
    private NodePath spriteNodePath;

    [Export]
    private Shader outlineShader;

    [Export]
    private Color outlineColor;

    private Area2D hookReception;

    private Sprite hookSprite;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        hookSprite = GetNode<Sprite>(spriteNodePath);
        hookSprite.Material = (Material)hookSprite.Material.Duplicate();

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

    public override void SetSelected(bool newSelectedValue)
    {
        base.SetSelected(newSelectedValue);
        if(hookSprite.Material is ShaderMaterial shaderMaterial)
        {
            shaderMaterial.Shader = (IsSelected) ? outlineShader : null;
            shaderMaterial.SetShaderParam("outline_color", outlineColor);
        }
    }

}
