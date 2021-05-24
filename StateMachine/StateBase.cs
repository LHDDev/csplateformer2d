using Godot;
using System;

namespace TestCs.StateMachine.States
{
    public abstract class StateBase : Node
    {
        protected ActorBase actor;
        protected StateMachineBase finiteStateMachine;

        public abstract void Do();
        public virtual Boolean CanUpdateDirection()
        {
            return true;
        }

        public virtual Boolean CanHook()
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
