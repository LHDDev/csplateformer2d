using Godot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heimgaerd.StateMachine.States.NPCStates
{
    class NpcIdleState : IdleState
    {
        public override bool CanHook()
        {
            return false;
        }
        public override void Do()
        {
            if (actor.FacingDirection != 0)
            {
                finiteStateMachine.ChangeState<MoveState>();
            }
        }

        public override void EnterState()
        {
            //actor.ActorSprite.Play("idle");
            actor.Velocity.y = 0;
        }
    }
}
