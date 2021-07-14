using Godot;
using Heimgaerd.StateMachine;
using System;

namespace Heimgaerd.StateMachine.States
{
    public class WallState : StateBase
    {
        public override bool CanUpdateDirection()
        {
            return false;
        }

        public override void Do()
        {
            int pressedDirection = Math.Sign(Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"));

            if (pressedDirection == 0 || pressedDirection == actor.FacingDirection)
            {
                finiteStateMachine.ChangeState<FallState>();
            }

            if (Input.IsActionJustPressed("g_jump"))
            {
                finiteStateMachine.ChangeState<JumpState>();
            }
        }

        public override void EnterState()
        {
            (actor as ActorPlayer).ReverseDirection();
            actor.Velocity = Vector2.Zero;
            actor.ActorSprite.Play("wall_slide");
        }
    }
}