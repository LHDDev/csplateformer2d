using Godot;
using Heimgaerd.StateMachine;
using Heimgaerd.StateMachine.States.AttackStates;
using System;

namespace Heimgaerd.StateMachine.States
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

            if (Input.GetActionStrength("ui_left") - Input.GetActionStrength("ui_right") != 0 && Input.IsActionJustPressed("g_attack") && actor.CurrentStamina > 0)
            {
                finiteStateMachine.ChangeState<DashAttack>();
            }
        }

        public override void EnterState()
        {
            actor.ActorSprite.Play("run");
        }
    }
}