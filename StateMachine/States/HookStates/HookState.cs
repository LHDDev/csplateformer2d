
using Godot;
using Heimgaerd.StateMachine;
using Heimgaerd.StateMachine.States;

namespace TestCs.StateMachine.States.HookStates
{
    class HookState : StateBase
    {
        [Export]
        private Vector2 hookPullingMovement;
        private Vector2 positionTo;
        
        public override void Do()
        {
            actor.Velocity = (positionTo - actor.Position).Normalized();
            actor.Velocity *= hookPullingMovement;
            if(actor.Position.y <= positionTo.y)
            {

                actor.isSnapped = true;
                finiteStateMachine.ChangeState<FallState>();
            }
        }

        public override void EnterState()
        {
            actor.ActorSprite.Play("jump");
            positionTo = (actor as ActorPlayer).HookDetectionArea.GetSelectedHookableGlobalPosition();
            actor.isSnapped = false;
        }

        public override bool CanUpdateDirection()
        {
            return false;
        }
    }
}
