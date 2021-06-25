using Godot;
using System;

public class Hook : KinematicBody2D
{
    [Export]
    private Vector2 movementSpeed;

    private Vector2 directionTo;
    private Vector2 velocity;
    public Vector2 DirectionTo { get => directionTo; set => directionTo = value; }

    [Signal]
    public delegate void HookableCollided();

    public override void _Ready()
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        if (this.GlobalPosition.DistanceTo(directionTo) >= 2.0f)
        {
            this.velocity = (directionTo - this.GlobalPosition).Normalized();
            this.velocity.x *= this.movementSpeed.x;
            this.velocity.y *= this.movementSpeed.y;

            MoveAndSlideWithSnap(velocity, Vector2.Zero ,Vector2.Up) ;
        }
        else
        {
            EmitSignal(nameof(HookableCollided));
            QueueFree();
        }
    }

}
