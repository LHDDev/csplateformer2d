using Godot;
using System;

namespace TestCs.StateMachine.States
{
    public class RunState : StateBase
    {
        public override void Do()
        {
            if (actor.FacingDirection == 0)
            {
                finiteStateMachine.ChangeState<IdleState>();
            }

            actor.Velocity.x = actor.movementSpeed * actor.FacingDirection;
            actor.Velocity.y = 0;

            if (Input.IsActionJustPressed("g_jump"))
            {
                finiteStateMachine.ChangeState<JumpState>();
            }

            if (!actor.IsOnFloor())
            {
                // Active Coyotte Time
                finiteStateMachine.ChangeState<FallState>();
            }
        }

        public override void EnterState()
        {
            actor.ActorSprite.Play("run");
        }
    }
}