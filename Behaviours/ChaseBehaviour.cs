using Godot;
using System;

namespace Heimgaerd.Behaviours
{
    public class ChaseBehaviour : BehaviourBase
    {
        private Vector2 currentTargetPos;
        private Physics2DDirectSpaceState spaceState;
        private Godot.Collections.Array exceptions;

        public override void _Ready()
        {
            base._Ready();
            spaceState = actor.GetWorld2d().DirectSpaceState;
            exceptions = new Godot.Collections.Array();
            exceptions.Add(actor);
        }
        public override void Do()
        {
            if(currentTargetPos != actor.currentTarget.GlobalPosition)
            {
                currentTargetPos = actor.currentTarget.GlobalPosition;
                UpdateFacingTarget();
                Godot.Collections.Dictionary result = spaceState.IntersectRay(actor.GlobalPosition, currentTargetPos, exceptions, actor.CollisionMask, true,false);
                if(!(result["collider"] is ActorPlayer))
                {
                    behaviourMachine.ChangeBehaviour<PatrolBehaviour>();
                }
            }
        }

        public override void EnterBehaviour()
        {
            currentTargetPos = actor.currentTarget.GlobalPosition;
        }
        public void UpdateFacingTarget()
        {
            actor.UpdateFacingDirection(Math.Sign(currentTargetPos.x - actor.GlobalPosition.x));
        }
    }

}