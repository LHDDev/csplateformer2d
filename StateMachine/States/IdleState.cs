using Godot;
using Heimgaerd.StateMachine;
using Heimgaerd.StateMachine.States.AttackStates;
using System;

namespace Heimgaerd.StateMachine.States
{
    public class IdleState : StateBase
    {
        public override void Do()
        {
            actor.Velocity.x = Mathf.Lerp(actor.Velocity.x, 0f, 0.2f);

            if ((Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right")) && Input.GetActionStrength("ui_left") - Input.GetActionStrength("ui_right") != 0)
            {
                finiteStateMachine.ChangeState<RunState>();
            }
            if (Input.IsActionJustPressed("g_jump"))
            {
                finiteStateMachine.ChangeState<JumpState>();
            }
            if (Input.IsActionJustPressed("g_attack") && actor.CurrentStamina > 0)
            {
                finiteStateMachine.ChangeState<GroundAttack>();
            }
            if (!actor.IsOnFloor())
            {
                finiteStateMachine.ChangeState<FallState>();
            }
        }

        public override void EnterState()
        {
            actor.ActorAnimation.Play("mc-RESET");
            actor.ActorSprite.Play("idle");
            actor.Velocity.y = 0;
        }
    }
}
