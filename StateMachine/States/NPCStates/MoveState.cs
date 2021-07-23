using Godot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heimgaerd.StateMachine.States.NPCStates
{
    class MoveState : StateBase
    {
        public override bool CanHook()
        {
            return false;
        }

        public override void Do()
        {
            if (actor.FacingDirection == 0)
            {
                finiteStateMachine.ChangeState<NpcIdleState>();
            }

            actor.Velocity.x = actor.movementSpeed * actor.FacingDirection;
            actor.Velocity.y = 0;
        }

        public override void EnterState()
        {
            //actor.ActorSprite.Play("run");
        }
    }
}
