using Godot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heimgaerd.Behaviours
{
    public abstract class BehaviourBase : Node
    {
        protected ActorEnnemyBase actor;
        protected BehaviourMachine behaviourMachine;

        public override void _Ready()
        {
            actor = GetOwner<ActorEnnemyBase>();
            behaviourMachine = GetParent<BehaviourMachine>();
        }

        public abstract void EnterBehaviour();
        public abstract void Do();
    }
}
