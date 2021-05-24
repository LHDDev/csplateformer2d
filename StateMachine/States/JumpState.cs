using Godot;
using System;

namespace TestCs.StateMachine.States
{
    public class JumpState : StateBase
    {
        public override void Do()
        {
            if (actor.FacingDirection != 0)
            {
                actor.Velocity.x = actor.movementSpeed * actor.FacingDirection;
                if (actor.IsOnWall() && !actor.IsOnFloor())
                {
                    actor.isSnapped = true;
                    finiteStateMachine.ChangeState<WallState>();
                }
            }

            if (actor.Velocity.y > 0)
            {
                actor.isSnapped = true;
                finiteStateMachine.ChangeState<FallState>();
            }
        }

        public override void EnterState()
        {
            actor.ActorSprite.Play("jump");
            actor.Velocity.y = actor.JumpForce;
            actor.isSnapped = false;
        }

    }
}