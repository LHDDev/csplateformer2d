using Godot;
using Heimgaerd.StateMachine;
using System;

namespace Heimgaerd.StateMachine.States
{
    public class DeathState : StateBase
    {
        public override void Do()
        {
            if (((ActorPlayer)actor).CanRespanw)
            {
                actor.Death();
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
            actor.ActorAnimation.Play("death");
            actor.Velocity.x = 0;
        }
    }
}
