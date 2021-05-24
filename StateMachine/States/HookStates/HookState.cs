using System;
using Godot;
using System.Collections.Generic;
using System.Text;

namespace TestCs.StateMachine.States.HookStates
{
    class HookState : StateBase
    {
        private Vector2 positionTo;
        public override void Do()
        {
            actor.Velocity = (positionTo - actor.Position).Normalized();
            actor.Velocity.x *= actor.movementSpeed;
            actor.Velocity.y *= -actor.JumpForce ;
            if(actor.Position.y <= positionTo.y)
            {
                actor.isSnapped = true;
                finiteStateMachine.ChangeState<FallState>();
            }
        }

        public override void EnterState()
        {
            actor.ActorSprite.Play("jump");
            positionTo = (actor as ActorPlayer).HookDetectionArea.getSelectedHookableGlobalPosition();
            actor.isSnapped = false;
        }
    }
}
