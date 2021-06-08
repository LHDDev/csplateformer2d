
using Godot;
using TestCs.StateMachine.States.HookStates;

namespace TestCs.StateMachine.States
{
    public class FallState : StateBase
    {
        private int initialFacingDirection;
        public override void Do()
        {
            if (!(finiteStateMachine.PreviousState is WallState))
            {
                if (!actor.IsOnWall())
                {
                    GD.Print($"FacingDirection = {actor.FacingDirection}");
                    actor.Velocity.x = (actor.FacingDirection == 0 ) ? actor.movementSpeed * initialFacingDirection:actor.movementSpeed*actor.FacingDirection;
                    
                }
                else if (!actor.IsOnFloor())
                {
                    finiteStateMachine.ChangeState<WallState>();
                }
            }

            if (actor.IsOnFloor())
            {
                finiteStateMachine.ChangeState<IdleState>();
            }

        }

        public override void EnterState()
        {
            actor.ActorSprite.Play("fall");
            initialFacingDirection = (finiteStateMachine.PreviousState is HookState)?((actor.ActorSprite.FlipH)?-1:1):actor.FacingDirection;
        }
    }
}