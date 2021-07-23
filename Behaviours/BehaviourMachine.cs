using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heimgaerd.Behaviours
{
    class BehaviourMachine : Node
    {
        private BehaviourBase currentBehaviour;

        public override void _Process(float delta)
        {
            currentBehaviour.Do();
        }

        public void ChangeBehaviour<T>() where T : BehaviourBase
        {
            currentBehaviour = this.FindChildrenOfType<T>().First();
            currentBehaviour.EnterBehaviour();
        }
    }
}
