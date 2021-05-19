using Godot;
using System;

namespace TestCs.StateMachine.States
{
    public class DeathState : StateBase
    {
        public override void Do()
        {
            if (!actor.IsInGroup("player"))
            {

                actor.QueueFree();
            }
            else
            {
                // Launch Game Over Scene
            }

        }

        public override bool CanUpdateDirection()
        {
            return false;
        }

        public override void EnterState()
        {
            actor.ActorSprite.Play("death");
        }
    }
}
