using Godot;
using Heimgaerd.StateMachine;
using System;

namespace Heimgaerd.StateMachine.States
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
        public override bool CanHook()
        {
            return false;
        }

        public override void EnterState()
        {
            actor.ActorSprite.Play("death");
        }
    }
}
