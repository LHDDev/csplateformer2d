using Godot;
using System;

namespace Heimgaerd.StateMachine
{
    public abstract class StateBase : Node
    {
        protected ActorBase actor;
        protected StateMachineBase finiteStateMachine;

        public abstract void Do();
        public virtual bool CanUpdateDirection()
        {
            return true;
        }

        public virtual bool CanHook()
        {
            return true;
        }

        public virtual bool CanMove()
        {
            return true;
        }
        public abstract void EnterState();

        public override void _Ready()
        {
            actor = GetOwner<ActorBase>();
            finiteStateMachine = GetParent<StateMachineBase>();
        }
    }
}
