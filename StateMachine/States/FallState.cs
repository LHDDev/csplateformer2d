
namespace TestCs.StateMachine.States
{
    public class FallState : StateBase
    {
        public override void Do()
        {
            if (!(finiteStateMachine.PreviousState is WallState) && actor.FacingDirection != 0)
            {
                if (!actor.IsOnWall())
                {
                    actor.Velocity.x = actor.movementSpeed * actor.FacingDirection;
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
        }
    }
}