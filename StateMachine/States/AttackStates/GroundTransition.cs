using Godot;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCs.StateMachine.States.AttackStates
{
    class GroundTransition : StateBase
    {
        int framesBeforeIdle;
        public override void Do()
        {
            
            
            if(!actor.ActorAnimation.IsPlaying() || actor.CurrentStamina <= 0 )
            {
                finiteStateMachine.ChangeState<IdleState>();
            }
        }

        public override void EnterState()
        {
            actor.ActorAnimation.Play("mc-ground-transition");
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if(finiteStateMachine.CurrentState is GroundTransition)
            {

                if (Input.IsActionJustPressed("g_jump"))
                {
                    finiteStateMachine.ChangeState<JumpState>();
                }

                if (actor.CurrentStamina > 0)
                {
                    if (Input.IsActionJustPressed("g_attack") && actor.CurrentComboID < actor.MaxGroundCombo)
                    {
                        finiteStateMachine.ChangeState<GroundAttack>();
                    }

                    if(actor.CurrentDashComboID < actor.MaxDashCombo && Input.GetActionStrength("ui_left") - Input.GetActionStrength("ui_right") != 0 && Input.IsActionJustPressed("g_attack"))
                    {
                        finiteStateMachine.ChangeState<DashAttack>();
                    }
                }


            }
        }
    }
}
